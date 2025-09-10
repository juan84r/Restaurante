using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Aplication.UseCase.Restaurante.Create.Models;

namespace Aplication.Interfaces
{
    public interface ICategoryServices
    {
        Task <Category> CreateCategory(CreateCategoryRequest request);

        Task <Category> DeleteCategory(int CategoryId);

        Task <List<Category>> GetAll();

        Task <Category> GetById(int CategoryId);
    }
}