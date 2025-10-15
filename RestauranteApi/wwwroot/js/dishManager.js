document.addEventListener('DOMContentLoaded', () => {
    // Escucha el evento de envío del formulario de creación
    const form = document.getElementById('createDishForm');
    if (form) {
        form.addEventListener('submit', createDish);
    }
});

// Funcion para cargar y listar todos los platos
async function loadDishes() {
    const dishList = document.getElementById('dishList');
    
    dishList.innerHTML = '<p class="text-center w-100">Cargando platos...</p>';
    
    try {
        const res = await fetch(`${API_URL}/dish`);
        const dishes = await res.json();
        
        dishList.innerHTML = '';
        
        if (dishes.length === 0) {
            dishList.innerHTML = '<p class="text-center w-100 text-muted">No hay platos registrados.</p>';
            return;
        }

        dishes.forEach(dish => {
            const dishGuid = dish.Id || dish.dishId || dish.id; 
            
            // Define la URL de la imagen (con placeholder si falta)
            const imageUrl = dish.imageUrl || 'https://via.placeholder.com/286x200?text=Plato';


            const card = `
                <div class="col-12 col-sm-6 col-md-4 col-lg-3 mb-4">
                    <div class="card shadow h-100"> 
                        <img src="${imageUrl}" 
                             class="card-img-top" alt="${dish.name}" 
                             style="height: 200px; object-fit: cover;">
                        
                        <div class="card-body d-flex flex-column"> 
                            
                            <div>
                                <h5 class="card-title"><strong>${dish.name}</strong></h5>
                                <p class="card-text">${dish.description.substring(0, 70)}...</p>
                            </div>
                            
                            <div class="mt-auto"> 
                                <p class="mb-1"><strong>$${dish.price.toFixed(2)}</strong></p>
                                <p class="card-text small text-muted mb-2">ID: ${dishGuid.substring(0, 8)}...</p>
                            </div>
                            
                            <button class="btn btn-danger w-100" onclick="deleteDish('${dishGuid}')">
                                Eliminar Plato
                            </button>
                        </div>
                    </div>
                </div>`;
                
            dishList.innerHTML += card;
        });

    } catch (err) {
        console.error('Error cargando platos:', err);
        showMessage('Error al cargar la lista de platos.', 'danger');
        dishList.innerHTML = '<p class="text-danger w-100">Error al conectar con la API.</p>';
    }
}

// Funcion para crear un nuevo plato (C - Create)
async function createDish(event) {
    event.preventDefault();
    
    const dish = {
        name: document.getElementById('dishName').value,
        price: parseFloat(document.getElementById('dishPrice').value),
        description: document.getElementById('dishDescription').value,
        categoryId: parseInt(document.getElementById('dishCategory').value),
        imageUrl: document.getElementById('dishImageUrl').value || 'placeholder'
    };
    
    try {
        const res = await fetch(`${API_URL}/dish`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dish)
        });

        if (res.ok) {
            showMessage('Plato creado con éxito.', 'success');
            document.getElementById('createDishForm').reset();
            loadDishes(); // Recargar la lista
        } else {
            const errorData = await res.json();
            showMessage(`Error al crear: ${errorData.message || res.statusText}`, 'danger');
        }

    } catch (err) {
        showMessage('Error de conexión al crear el plato.', 'danger');
        console.error('Error:', err);
    }
}

// Funcion para eliminar un plato (D - Delete)
async function deleteDish(dishId) {
    if (!confirm(`¿Estás seguro de que quieres eliminar el plato ID ${dishId}?`)) {
        return;
    }

    try {
        const res = await fetch(`${API_URL}/dish/${dishId}`, {
            method: 'DELETE',
        });

        if (res.ok || res.status === 204) {
            showMessage(`Plato ID ${dishId} eliminado correctamente.`, 'success');
            loadDishes(); // Recargar la lista
        } else {
            showMessage(`Error al eliminar el plato ID ${dishId}.`, 'danger');
        }

    } catch (err) {
        showMessage('Error de conexión al eliminar el plato.', 'danger');
        console.error('Error:', err);
    }
}