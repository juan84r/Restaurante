using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Order
{
    public long OrderId { get; set; }
    public int DeliveryTypeId { get; set; }   // FK
    public int OverallStatusId { get; set; }  // FK
    public string DeliveryTo { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

    // Relaciones
    public DeliveryType DeliveryType { get; set; } = null!;
    public Status OverallStatus { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}