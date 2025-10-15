// Mapeo de estados y la accion siguiente
const STATUS_TRANSITIONS = {
    // ID: { Nombre en español, Siguiente ID, Texto del boton, Clase de color }
    1: { name: "Pendiente", nextId: 2, nextText: "Pasar a Preparación", class: "btn-warning" },
    2: { name: "En preparación", nextId: 3, nextText: "Marcar Listo", class: "btn-info" },
    3: { name: "Lista para Entregar", nextId: 4, nextText: "Marcar Entregado", class: "btn-success" },
    4: { name: "Entregada", nextId: 4, nextText: "Orden Finalizada", class: "btn-secondary" } // Estado final, no avanza
};

// Mapeo de tipos de entrega (para display)
const DELIVERY_TYPE_MAP = {
    "Dine in": "En mesa",
    "Take away": "Retiro en mostrador",
    "Delivery": "Delivery"
};

// 1. CARGAR TODAS LAS ORDENES
async function loadAllOrders() {
    const panel = document.getElementById("ordersPanel");
    panel.innerHTML = "<p class='text-center w-100'>Cargando órdenes...</p>";

    try {
        const res = await fetch(`${API_URL}/order`);
        const orders = await res.json();

        panel.innerHTML = "";

        if (!orders || orders.length === 0) {
            panel.innerHTML = "<p class='text-center w-100 text-muted'>No hay órdenes registradas.</p>";
            return;
        }

        orders.forEach(order => {
            
            // Logica de estado y accion
            const currentStatusId = order.overallStatusId || 1; 
            const currentStatusData = STATUS_TRANSITIONS[currentStatusId] || STATUS_TRANSITIONS[1];
            
            const nextAction = currentStatusData.nextText;
            const nextStatusId = currentStatusData.nextId;
            const buttonClass = currentStatusData.class;
            const isDisabled = currentStatusId === 4 ? 'disabled' : '';
            
            // Traduccion de tipo y estado para el display
            const tipo = DELIVERY_TYPE_MAP[order.deliveryTypeName] || order.deliveryTypeName || "Sin tipo";
            const estado = currentStatusData.name;
            
            // Construccion de lista de platos
            let platosHTML = "";
            if (order.items && order.items.length > 0) {
                platosHTML = "<ul class='list-group list-group-flush mb-3 mt-1'>"; 
                order.items.forEach(item => {
                    platosHTML += `
                        <li class="list-group-item d-flex justify-content-between align-items-start py-1">
                            <div>
                                <strong>${item.dishName}</strong> (x${item.quantity})
                                ${item.note ? `<br><small class="text-muted">Nota: ${item.note}</small>` : ""}
                            </div>
                        </li>`;
                });
                platosHTML += "</ul>";
            } else {
                platosHTML = "<p class='text-muted'>Sin platos registrados.</p>";
            }

            // Verificacion de la fecha para evitar "Invalid Date"
            let formattedDate = '';
            if (order.createDate && new Date(order.createDate).getTime()) {
                formattedDate = new Date(order.createDate).toLocaleString();
            }

            // Construccion de la tarjeta visual
            const card = document.createElement("div");
            card.className = "col-12 col-sm-6 col-md-4 col-lg-3 mb-4"; 
            
            card.innerHTML = `
                <div class="card shadow h-100">
                    <div class="card-header bg-light">
                        <h5 class="mb-0"><strong>Orden #${order.orderId}</strong></h5>
                    </div>
                    
                    <div class="card-body d-flex flex-column"> 
                        
                        <div>
                            <p class="mb-3"><strong>Tipo:</strong> ${tipo}</p>
                            <p class="mb-3"><strong>Estado actual:</strong> 
                                <span class="badge bg-${buttonClass.split('-')[1]}">${estado}</span>
                            </p>

                            <h6>Platos:</h6>
                            ${platosHTML}
                        </div>

                        <div class="mt-auto pt-2">
                            ${formattedDate ? `<small class="text-muted d-block mb-2">Creada: ${formattedDate}</small>` : ''}
                            <button class="btn ${buttonClass} w-100" ${isDisabled} onclick="updateStatus(${order.orderId}, ${nextStatusId})">
                                ${nextAction}
                            </button>
                        </div>
                    </div>
                </div>`;
            panel.appendChild(card);
        });
    } catch (err) {
        console.error("Error cargando órdenes:", err);
        showMessage("Error cargando órdenes", "danger");
    }
}

// 2. ACTUALIZAR ESTADO DE UNA ORDEN COMPLETA (sin cartel de confirmacion)
async function updateStatus(orderId, newStatus) {
    
    try {
        // Intento 1: actualizar estado de toda la orden
        let res = await fetch(`${API_URL}/order/${orderId}/status`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ statusId: newStatus })
        });

        // Manejo de error o fallback (si el endpoint principal falla)
        if (!res.ok) {
            console.warn("Endpoint /order/{id}/status no disponible, intentando actualizar por ítems...");
            
            // Fallback: Si la API no permite el cambio global, se intenta item por item
            const resOrder = await fetch(`${API_URL}/order/${orderId}`);
            const order = await resOrder.json();

            for (const item of order.items || order.orderItems || []) { 
                await fetch(`${API_URL}/order/${orderId}/items/${item.orderItemId}/status`, {
                    method: "PUT",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ statusId: newStatus })
                });
            }
        }

        showMessage("Estado actualizado correctamente", "success");
        await loadAllOrders(); // Recargar el panel para ver el cambio
        
    } catch (err) {
        console.error("Error al actualizar estado:", err);
        showMessage("Error al actualizar estado", "danger");
    }
}