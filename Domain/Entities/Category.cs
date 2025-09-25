using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
   
    [StringLength(25, ErrorMessage = "El nombre no puede superar los 25 caracteres")]
    public string Name { get; set; } = string.Empty;

    [StringLength(255, ErrorMessage = "La descripción no puede superar los 255 caracteres")]
    public string Description { get; set; } = string.Empty;

    public int Order { get; set; } = 0;  // opcional según consigna

    // Relacion
    public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
