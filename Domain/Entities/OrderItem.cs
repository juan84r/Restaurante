using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class OrderItem
{
    [Key]
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long OrderItemId { get; set; }
    public long OrderId { get; set; }   // FK
    public Guid DishId { get; set; }    // FK
    public int Quantity { get; set; }

    [MaxLength(500)]
    public string Notes { get; set; } = string.Empty;
    public int StatusId { get; set; }   // FK
    public DateTime CreateDate { get; set; }

    // Relaciones
    public Order Order { get; set; } = null!;
    public Dish Dish { get; set; } = null!;
    public Status Status { get; set; } = null!;
}