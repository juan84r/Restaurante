using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication;
using Domain.Entities;

namespace Aplication.Interfaces
{
    public interface IOrderQuery
    {
        Order? GetOrderById(long id);
        List<Order> GetOrders(DateTime? dateFrom = null, DateTime? dateTo = null, int? statusId = null);
    }

}