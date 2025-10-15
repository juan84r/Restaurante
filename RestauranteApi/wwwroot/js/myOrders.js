async function loadMyOrders() {
  const container = document.getElementById("ordersContainer");
  container.innerHTML = "<p>Cargando...</p>";

  try {
    const res = await fetch(`${API_URL}/order`);
    const orders = await res.json();

    const clientName = getClientName();
    const myOrders = orders.filter(o => (o.clientName || "").toLowerCase() === clientName.toLowerCase());

    container.innerHTML = "";

    if (myOrders.length === 0) {
      container.innerHTML = "<p>No tenés comandas registradas.</p>";
      return;
    }

    myOrders.forEach(order => {
      let total = 0;
      if (order.orderItems) {
        total = order.orderItems.reduce((sum, i) => sum + (i.dish?.price || 0) * i.quantity, 0);
      }

      const card = `
        <div class="col-md-4">
          <div class="card shadow-sm mb-3">
            <div class="card-body">
              <h5>Orden #${order.orderId}</h5>
              <p>Fecha: ${formatDate(order.createDate)}</p>
              <p>Entrega: ${order.delivery?.deliveryTypeName || "N/A"}</p>
              <p>Estado: <strong>${order.overallStatus?.name || "Pendiente"}</strong></p>
              <p>Total: ${formatPrice(total)}</p>
            </div>
          </div>
        </div>`;
      container.innerHTML += card;
    });
  } catch (err) {
    console.error(err);
    showMessage("Error cargando tus comandas", "danger");
  }
}
