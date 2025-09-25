using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplication
{
    public class CreateOrderItemRequest
    {
        [Required]
        public Guid DishId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;

        public string? Note { get; set; }
    }

    public class CreateOrderRequest
    {
        [Required]
        public int DeliveryTypeId { get; set; }

        public int? TableNumber { get; set; }
        public string? ClientName { get; set; }
        public string? Address { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Debe incluir al menos un item")]
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }
}
