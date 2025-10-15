using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplication;

namespace Aplication.Interfaces
{
    public interface IOrderServices
    {
        Task<OrderResponse> CreateOrder(CreateOrderRequest request);
        Task<List<OrderResponse>> GetOrders(DateTime? dateFrom = null, DateTime? dateTo = null, int? statusId = null);
        Task<OrderResponse> GetOrderById(long id);
        Task<CreateOrderItemResponse> AddOrderItem(long orderId, CreateOrderItemRequest request);
        Task<CreateOrderItemResponse> UpdateOrderItemStatus(long orderId, long itemId, CreateOrderItemStatusRequest request);
        Task DeleteOrderItem(long orderId, long itemId);

         Task<bool> UpdateOrderStatus(long orderId, int newStatusId);
    }
}