using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication.UseCase.Restaurante.Create.Models;
using Aplication.Response;

namespace Aplication.Interfaces
{
    public interface IDishServices
    {
        Task <CreateDishResponse> CreateDish(CreateDishRequest request);

        Task <Dish> DeleteDish(int DishId);

        Task <List<Dish>> GetAll();

        Task <CreateDishResponse> GetById(Guid DishId);
    }
}