using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Aplication.UseCase.Restaurante.Create.Models;
using Aplication.Response;


namespace Aplication.UseCase.Restaurante
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
            var dish = new Dish
            {
                DishId = request.DishId,
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

        public Task <List<Dish>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CreateDishResponse> GetById(Guid dishId)
        {
            var dish = _query.GetDish(dishId);
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
    }
}