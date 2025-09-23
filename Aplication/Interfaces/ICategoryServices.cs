using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication;

namespace Aplication.Interfaces
{
    public interface ICategoryServices
    {
        Task <CreateCategoryResponse> CreateCategory(CreateCategoryRequest request);

        Task <Category> DeleteCategory(int CategoryId);

        Task <List<Category>> GetAll();

        Task <CreateCategoryResponse> GetById(int CategoryId);
    }
}