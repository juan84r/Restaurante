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
    public class CategoryQuery : ICategoryQuery
    {
        private readonly AppDbContext _context;
        public CategoryQuery(AppDbContext context)
        {
            _context = context;
        }
        public List<Category> GetListCategory()
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(int categoryId)
        {
            var category = _context.Categories
                .FirstOrDefault(s => s.Id == categoryId);

            if (category == null)
                throw new Exception($"Categoría con ID {categoryId} no encontrada.");

            return category;
        }
    }
}