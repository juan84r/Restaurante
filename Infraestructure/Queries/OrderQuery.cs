using System;
using System.Collections.Generic;
using System.Linq;
using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Queries
{
    public class OrderQuery : IOrderQuery
    {
        private readonly AppDbContext _context;
        public OrderQuery(AppDbContext context)
        {
            _context = context;
        }

        public Order? GetOrderById(long id)
        {
            return _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Dish)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Status)
                .Include(o => o.Delivery)
                .Include(o => o.OverallStatus)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public List<Order> GetOrders(DateTime? dateFrom = null, DateTime? dateTo = null, int? statusId = null)
        {
            var q = _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Dish)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Status)
                .Include(o => o.Delivery)
                .Include(o => o.OverallStatus)
                .AsQueryable();

            if (dateFrom.HasValue) q = q.Where(o => o.CreateDate >= dateFrom.Value);
            if (dateTo.HasValue) q = q.Where(o => o.CreateDate <= dateTo.Value);
            if (statusId.HasValue) q = q.Where(o => o.OverallStatusId == statusId.Value);
           
            return q.ToList();
        }
    }
}