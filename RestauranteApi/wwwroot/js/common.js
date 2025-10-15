const API_URL = "http://localhost:5014/api";

function formatPrice(value) {
  return `$${parseFloat(value || 0).toFixed(2)}`;
}

function formatDate(dateStr) {
  const date = new Date(dateStr);
  return date.toLocaleDateString('es-AR', {
    year: 'numeric', month: '2-digit', day: '2-digit',
    hour: '2-digit', minute: '2-digit'
  });
}

function showMessage(message, type = "info") {
  const alertBox = document.createElement("div");
  alertBox.className = `alert alert-${type} position-fixed top-0 start-50 translate-middle-x mt-3`;
  alertBox.style.zIndex = "2000";
  alertBox.textContent = message;
  document.body.appendChild(alertBox);
  setTimeout(() => alertBox.remove(), 3000);
}

function saveCurrentOrderId(orderId) {
  localStorage.setItem("currentOrderId", orderId);
}

function getCurrentOrderId() {
  return localStorage.getItem("currentOrderId");
}

function setClientName(name) {
  localStorage.setItem("clientName", name);
}

function getClientName() {
  return localStorage.getItem("clientName") || "Cliente";
}

function translateStatus(statusName) {
  const map = {
    "Pending": "Pendiente",
    "In Preparation": "En preparación",
    "Ready": "Listo",
    "Delivered": "Entregado"
  };
  return map[statusName] || statusName;
}
