using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infraestructure.Persistence;
using Aplication.Interfaces;

namespace Infraestructure.Command
{
    public class DishCommand : IDishCommand
    {
        private readonly AppDbContext _context;

        public DishCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertDish(Dish dish)
        {
            _context.Add(dish);

            await _context.SaveChangesAsync();
        }

        public Task RemoveDish(int dishId)
        {
            throw new NotImplementedException();
        }

    }
}