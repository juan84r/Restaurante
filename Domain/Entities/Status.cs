using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Status
{ 
    [Key]
    public int Id { get; set; }
    
    [StringLength(25, ErrorMessage = "El nombre no puede superar los 25 caracteres")]
    public string Name { get; set; } = string.Empty;

    // Relacion
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

