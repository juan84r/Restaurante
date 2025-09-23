using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication;
using Domain.Entities;

namespace Aplication.Interfaces
{
    public interface IDishQuery
    {
        Dish? GetDish(Guid id);
        List<Dish> GetAllDishes();
    }

}