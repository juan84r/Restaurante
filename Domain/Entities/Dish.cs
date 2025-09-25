using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Dish
{
    [Key]
    public Guid DishId { get; set; }      // UUID PK

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(255, ErrorMessage = "El nombre no puede superar los 255 caracteres")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor que 0")]
    public decimal Price { get; set; }

    public bool Available { get; set; } = true;

    [Required(ErrorMessage = "Debe tener una categoría asociada")]
    public int CategoryId { get; set; }   // FK

    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;  

    // Relacion
    public Category Category { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
