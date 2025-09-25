using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Order
{
    [Key]
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long OrderId { get; set; }
    public int DeliveryId { get; set; }   // FK
    public int OverallStatusId { get; set; }  // FK
      
    [StringLength(255, ErrorMessage = "El nombre no puede superar los 255 caracteres")]
    public string DeliveryTo { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    // Relaciones
    public Delivery? Delivery { get; set; }
    public Status? OverallStatus { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}