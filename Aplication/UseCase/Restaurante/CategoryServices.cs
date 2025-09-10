using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Domain.Models;


namespace Aplication.UseCase.Restaurante
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryCommand _command;
        private readonly ICategoryQuery _query;

        public CategoryServices(ICategoryCommand command, ICategoryQuery query)
        {
            _command = command;
            _query = query;
        }
        public async Task<Category> CreateCategory(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Id = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                Order = request.Order,
            };
            await _command.InsertCategory(category);
            return category;
        }

        public Task<Category> DeleteCategory(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task <List<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetById(int CategoryId)
        {
            throw new NotImplementedException();
        }
    }
}