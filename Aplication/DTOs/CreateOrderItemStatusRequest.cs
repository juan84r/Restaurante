using System.ComponentModel.DataAnnotations;

namespace Aplication
{
    public class CreateOrderItemStatusRequest
    {
        [Required]
        public int StatusId { get; set; }
    }
}