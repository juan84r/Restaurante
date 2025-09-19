using System;
using System.ComponentModel.DataAnnotations;

namespace Aplication.UseCase.Restaurante.Update.Models
{
    public class UpdateDishRequest
    {
        [Required]
        public Guid DishId { get; set; }   // El ID es obligatorio para actualizar

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor que 0")]
        public decimal Price { get; set; }

        public bool Available { get; set; } = true;

        [Required(ErrorMessage = "Debe asociar una categoría válida")]
        public int CategoryId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}
