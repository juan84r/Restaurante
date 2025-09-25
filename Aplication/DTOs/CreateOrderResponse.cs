using System;
using System.Collections.Generic;

namespace Aplication
{
    public class CreateOrderItemResponse
    {
        public long OrderItemId { get; set; }
        public Guid DishId { get; set; }
        public string DishName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string? Note { get; set; }
    }

    public class OrderResponse
    {
        public long OrderId { get; set; }
        public DateTime Date { get; set; }
        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; } = string.Empty;
        public int OverallStatusId { get; set; }
        public string OverallStatusName { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<CreateOrderItemResponse> Items { get; set; } = new();
    }
}
