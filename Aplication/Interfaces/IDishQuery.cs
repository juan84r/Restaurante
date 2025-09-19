using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Response;
using Domain.Entities;

namespace Aplication.Interfaces
{
    public interface IDishQuery
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<CreateDishResponse>> GetAllAsync(string? nameFilter, int? categoryId, string? sortByPrice);
        Task<CreateDishResponse?> GetByIdAsync(Guid id);
    }

}