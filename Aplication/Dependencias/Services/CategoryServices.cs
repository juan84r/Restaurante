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
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryCommand _command;
        private readonly ICategoryQuery _query;

        public CategoryServices(ICategoryCommand command, ICategoryQuery query)
        {
            _command = command;
            _query = query;
        }
        public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Id = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                Order = request.Order,
            };
            await _command.InsertCategory(category);
            return new CreateCategoryResponse
            {
                CategoryId = category.Id,
                Name = category.Name,
                Description = category.Description,
                Order = category.Order,
            };
        }

        public Task<Category> DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task <List<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CreateCategoryResponse> GetById(int categoryId)
        {
            var category = _query.GetCategory(categoryId);
            return Task.FromResult(new CreateCategoryResponse
            {
                CategoryId = category.Id,
                Name = category.Name,
                Description = category.Description,
                Order = category.Order,
            });
            
        }
    }
}