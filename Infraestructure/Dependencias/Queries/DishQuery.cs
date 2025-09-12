using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Querys
{
    public class DishQuery : IDishQuery
    {
        private readonly AppDbContext _context;
        public DishQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<Dish> GetListDish()
        {
            throw new NotImplementedException();
        }

        public Dish GetDish(Guid dishId)
        {
            var dish = _context.Dishes
                .FirstOrDefault(s => s.DishId == dishId);

            if (dish == null)
                throw new Exception($"Categoría con ID {dishId} no encontrada.");

            return dish;
        }
    }
}