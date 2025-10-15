let currentOrder = {
  deliveryTypeId: 1,
  clientName: "",
  address: "",
  items: []
};

// CARGAR EL MENU DE PLATOS
async function loadMenu() {
  try {
    const response = await fetch(`${API_URL}/dish`);
    const dishes = await response.json();

    const container = document.getElementById("menuContainer");
    container.innerHTML = "";

    dishes.forEach(dish => {
      // Define la URL de la imagen (con placeholder si falta)
      const imageUrl = dish.imageUrl || 'https://via.placeholder.com/286x200?text=Plato';
      
      const card = `
        <div class="col-12 col-sm-6 col-md-4 col-lg-3 mb-4">
          
          <div class="card shadow h-100"> 
            
            <img src="${imageUrl}" 
                 class="card-img-top" 
                 alt="${dish.name}"
                 style="height: 200px; object-fit: cover;">
                 
            <div class="card-body d-flex flex-column"> 
              
              <div> 
                  <h5 class="card-title"><strong>${dish.name}</strong></h5>
                  
                  <p class="card-text">${dish.description.substring(0, 70)}...</p> 
                  
              </div>
              
              <div class="mt-auto">
                <p class="mb-1"><strong>${formatPrice(dish.price)}</strong></p>
                <p class="mb-2">${dish.available ? "Disponible" : "No disponible"}</p>
              </div>

              <input type="text" id="note-${dish.dishId}" 
                     class="form-control form-control-sm mb-2" 
                     placeholder="Nota (opcional)">

              <button class="btn btn-primary w-100" 
                      onclick="addToOrder('${dish.dishId}', '${dish.name}', ${dish.price})" 
                      ${dish.available ? "" : "disabled"}>
                Agregar
              </button>
            </div>
          </div>
        </div>`;
      container.innerHTML += card;
    });
  } catch (err) {
    console.error("Error cargando platos:", err);
  }
}


// AGREGAR PLATOS AL PEDIDO
function addToOrder(dishId, name, price) {
  const noteInput = document.getElementById(`note-${dishId}`);
  const noteValue = noteInput ? noteInput.value.trim() : "";

  const existing = currentOrder.items.find(i => i.dishId === dishId);
  if (existing) {
    existing.quantity++;
    if (noteValue) existing.note = noteValue;
  } else {
    currentOrder.items.push({ dishId, name, price, quantity: 1, note: noteValue });
  }

  if (noteInput) noteInput.value = "";

  renderOrder();
}

// MOSTRAR LOS PLATOS AGREGADOS
function renderOrder() {
  const list = document.getElementById("orderList");
  const totalElem = document.getElementById("orderTotal");
  list.innerHTML = "";
  let total = 0;

  currentOrder.items.forEach(i => {
    const li = document.createElement("li");
    li.className = "list-group-item";
    li.innerHTML = `
      <div>
        <strong>${i.name}</strong> (x${i.quantity})
        ${i.note ? `<br><small>Nota: ${i.note}</small>` : ""}
      </div>
      <button class="btn btn-sm btn-danger float-end" onclick="removeFromOrder('${i.dishId}')">X</button>
    `;
    total += i.price * i.quantity;
    list.appendChild(li);
  });
  totalElem.textContent = total.toFixed(2);
}

// ELIMINAR UN PLATO DEL PEDIDO
function removeFromOrder(dishId) {
  currentOrder.items = currentOrder.items.filter(i => i.dishId !== dishId);
  renderOrder();
}

// MOSTRAR U OCULTAR CAMPOS SEGÚN ENTREGA
function toggleDeliveryFields() {
  const type = document.getElementById("deliveryType").value;
  document.getElementById("mesaField").style.display = (type === "1") ? "block" : "none";
  document.getElementById("direccionField").style.display = (type === "3") ? "block" : "none";
}

// ENVIAR PEDIDO AL SERVIDOR
async function sendOrder() {
  // Obtener valores limpios y el tipo de entrega seleccionado
  const deliveryTypeId = parseInt(document.getElementById("deliveryType").value);
  const clientName = document.getElementById("clientName").value.trim();
  const addressInput = document.getElementById("address")?.value.trim();
  const mesaInput = document.getElementById("tableNumber")?.value.trim();

  // 1. VALIDACION BASICA: Pedido vacio
  if (currentOrder.items.length === 0) {
    showMessage("Tu comanda está vacía", "warning");
    return;
  }

  // 2. VALIDACION DEL NOMBRE DEL CLIENTE (Obligatorio siempre)
  if (!clientName) {
    showMessage("Por favor, ingresa tu Nombre.", "danger");
    document.getElementById("clientName").focus();
    return;
  }

  // 3. VALIDACION CONDICIONAL Y ASIGNACION DE DIRECCION
  let address = "";
  
  if (deliveryTypeId === 1) { // Comer aqui (Requiere Mesa)
    if (!mesaInput) {
      showMessage("El Número de Mesa es obligatorio para consumo en local.", "danger");
      document.getElementById("tableNumber").focus();
      return;
    }
    address = `Mesa ${mesaInput}`;
    
  } else if (deliveryTypeId === 2) { // Retiro en mostrador (No requiere mas campos)
    address = "Retiro en mostrador";
    
  } else if (deliveryTypeId === 3) { // Delivery (Requiere Dirección)
    if (!addressInput) {
      showMessage("La Dirección es obligatoria para Delivery.", "danger");
      document.getElementById("address").focus();
      return;
    }
    address = addressInput;
  }
  
  // 4. PREPARAR Y ENVIAR EL OBJETO (Si la validacion fue exitosa)
  
  setClientName(clientName); 

  const orderToSend = {
    deliveryTypeId,
    clientName,
    address,
    items: currentOrder.items.map(i => ({
      dishId: i.dishId,
      quantity: i.quantity,
      note: i.note
    }))
  };

  try {
    const res = await fetch(`${API_URL}/order`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(orderToSend)
    });

    if (!res.ok) throw new Error("Error al enviar pedido");
    const data = await res.json();

    saveCurrentOrderId(data.orderId);
    showMessage("Comanda enviada correctamente", "success");

    // Limpiar el pedido despues del envio exitoso
    currentOrder.items = [];
    renderOrder();
  } catch (err) {
    console.error("Error enviando pedido:", err);
    showMessage("Error enviando la comanda", "danger");
  }
}