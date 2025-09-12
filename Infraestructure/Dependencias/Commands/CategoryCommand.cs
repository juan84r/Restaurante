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
    public class CategoryCommand : ICategoryCommand
    {
        private readonly AppDbContext _context;

        public CategoryCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertCategory(Category category)
        {
            _context.Add(category);

            await _context.SaveChangesAsync();
        }

        public Task RemoveCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

    }
}