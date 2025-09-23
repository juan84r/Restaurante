using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Aplication;
using Aplication.Exceptions;

namespace Aplication.Services
{
    public class DishServices : IDishServices
    {
        private readonly IDishCommand _command;
        private readonly IDishQuery _query;

        public DishServices(IDishCommand command, IDishQuery query)
        {
            _command = command;
            _query = query;
        }
        public async Task<CreateDishResponse> CreateDish(CreateDishRequest request)
        {
            // Verifica si el Nombre ya existe en el request.
            var existing = _query.GetAllDishes().FirstOrDefault(d => d.Name == request.Name);
            if (existing != null)
            {
                throw new DuplicateEntityException("Ya existe un plato con ese nombre.");
            }
            
            var dish = new Dish
            {
                DishId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Available = request.Available,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };
            await _command.InsertDish(dish);
            return new CreateDishResponse
            {
                DishId = dish.DishId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Available = dish.Available,
                CategoryId = dish.CategoryId,
                ImageUrl = dish.ImageUrl,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            };
        }

        public Task<Dish> DeleteDish(int dishId)
        {
            throw new NotImplementedException();
        }

         
            
        public async Task<List<Dish>> GetAll(string? name = null, int? categoryId = null, string? order = null)
        {
            var query = _query.GetAllDishes().AsQueryable();

            // Filtro por nombre
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(d => d.Name.Contains(name));
            }

            // Filtro por categoría
            if (categoryId.HasValue)
            {
                query = query.Where(d => d.CategoryId == categoryId.Value);
            }

            // Ordenamiento
            if (!string.IsNullOrWhiteSpace(order))
            {
                if (order.ToUpper() == "ASC")
                {
                    query = query.OrderBy(d => d.Price);
                }
                else if (order.ToUpper() == "DESC")
                {
                    query = query.OrderByDescending(d => d.Price);
                }
            }
            else
            {
                // Ordenamiento por defecto: primero categoría (asc), después precio (asc)
                query = query.OrderBy(d => d.CategoryId)
                            .ThenBy(d => d.Price);
            }

            return await Task.FromResult(query.ToList());
        }

        public Task<CreateDishResponse> GetById(Guid id)
        {
            var dish = _query.GetDish(id);
            if (dish == null) throw new NotFoundException($"Plato con id {id} no encontrado.");

            return Task.FromResult(new CreateDishResponse
            {
                DishId = dish.DishId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Available = dish.Available,
                CategoryId = dish.CategoryId,
                ImageUrl = dish.ImageUrl,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
            });

        }
        public async Task<CreateDishResponse?> UpdateDish(Guid id, CreateDishRequest request)
        {
            /**var other = _query.GetAllDishes().FirstOrDefault(d => d.Name == request.Name && d.DishId != id);
            if (other != null)
            throw new DuplicateEntityException("Ya existe otro plato con ese nombre.");**/

            // Llamar al comando para actualizar
            var updatedDish = await _command.UpdateDish(id, request);
            if (updatedDish == null) 
                throw new NotFoundException($"Plato con id {id} no encontrado.");


            return new CreateDishResponse
            {
                DishId = updatedDish.DishId,
                Name = updatedDish.Name,
                Description = updatedDish.Description,
                Price = updatedDish.Price,
                Available = updatedDish.Available,
                CategoryId = updatedDish.CategoryId,
                ImageUrl = updatedDish.ImageUrl,
                CreateDate = updatedDish.CreateDate,
                UpdateDate = updatedDish.UpdateDate,
            };
        }
    }
}