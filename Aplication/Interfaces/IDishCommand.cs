using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication;
using Domain.Entities;

namespace Aplication.Interfaces
{
    public interface IDishCommand
{
    Task InsertDish(Dish dish);
    Task <Dish?>UpdateDish(Guid id, CreateDishRequest request);
    Task DeleteDish(int dishId);
}

}