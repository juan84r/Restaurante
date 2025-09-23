using System;
using System.ComponentModel.DataAnnotations;

namespace Aplication
{
    public class CreateCategoryRequest
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres")]
        public string Description { get; set; } = string.Empty;

        public int Order { get; set; } = 0;  // opcional según consigna
    }
}
