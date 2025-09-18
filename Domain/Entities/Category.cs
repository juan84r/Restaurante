using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
    public string Name { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres")]
    public string Description { get; set; } = string.Empty;

    public int Order { get; set; } = 0;  // opcional según consigna

    // Relacion
    public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
