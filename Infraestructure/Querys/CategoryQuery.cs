using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;

namespace Infraestructure.Querys
{
    public class CategoryQuery : ICategoryQuery
    {
        public List<Category> GetListCategory()
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}