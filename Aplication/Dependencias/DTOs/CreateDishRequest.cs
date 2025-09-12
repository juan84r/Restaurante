using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCase.Restaurante.Create.Models
{
    public class CreateDishRequest
    {
        public Guid DishId { get; set; }           // ID del plato
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public int CategoryId { get; set; }        // FK a Category
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        
        public string? CategoryName { get; set; }
    }
}