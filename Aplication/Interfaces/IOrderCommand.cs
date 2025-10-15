using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication;
using Domain.Entities;

namespace Aplication.Interfaces
{
    public interface IOrderCommand
{
        Task InsertOrder(Order order);
        Task UpdateOrder(Order order);
        Task InsertOrderItem(OrderItem item);
        Task UpdateOrderItem(OrderItem item);
        Task DeleteOrderItem(OrderItem item);
        Task UpdateOrderStatus(long orderId, int newStatusId);

}

}