using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Dish
{
    public Guid DishId { get; set; }      // UUID PK
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool Available { get; set; }
    public int CategoryId { get; set; }   // FK
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    // Relacion
    public Category Category { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}