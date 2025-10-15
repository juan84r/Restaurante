using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Commands
{
    public class OrderCommand : IOrderCommand
    {
        private readonly AppDbContext _context;
        public OrderCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task InsertOrderItem(OrderItem item)
        {
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItem(OrderItem item)
        {
            _context.OrderItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItem(OrderItem item)
        {
            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatus(long orderId, int newStatusId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                throw new Exception($"No se encontró la orden con ID {orderId}");

            order.OverallStatusId = newStatusId;
            order.UpdateDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

    }
}