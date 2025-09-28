using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Commands;
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
            return _context.Dishes.ToList();
        }

        public List<Dish> GetAllDishes()
        {
            return _context.Dishes
                           .Include(d => d.Category)
                           .ToList();
        } 
      
        public Dish? GetDish(Guid id)
        {
            return _context.Dishes
                           .Include(d => d.Category)
                           .Include(d => d.OrderItems)
                           .FirstOrDefault(s => s.DishId == id);
        }
    }
}