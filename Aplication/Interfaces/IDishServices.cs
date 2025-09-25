using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication;

namespace Aplication.Interfaces
{
    public interface IDishServices
    {
        Task<CreateDishResponse> CreateDish(CreateDishRequest request);

        Task DeleteDish(Guid DishId);

        // Nuevo: GetAll con filtros y ordenamiento
        Task<List<CreateDishResponse>> GetAll(string? name = null, int? categoryId = null, string? order = null);

        Task<CreateDishResponse> GetById(Guid id);

        // Nuevo: actualizar un plato
        Task<CreateDishResponse?> UpdateDish(Guid id, CreateDishRequest request);
    }
}
