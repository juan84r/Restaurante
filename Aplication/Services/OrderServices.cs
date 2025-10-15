using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Aplication;
using Domain.Entities;
using Aplication.Exceptions;

namespace Aplication.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;
        private readonly IDishQuery _dishQuery; // ya existe en el proyecto

        public OrderServices(IOrderCommand command, IOrderQuery query, IDishQuery dishQuery)
        {
            _command = command;
            _query = query;
            _dishQuery = dishQuery;
        }

        public async Task<OrderResponse> CreateOrder(CreateOrderRequest request)
        {
            if (request == null) throw new BadRequestException("Request inválido.");
            if (request.Items == null || !request.Items.Any()) throw new BadRequestException("Debe incluir al menos un item.");

            const int initialStatusId = 1;

            var order = new Order
            {
               // OrderId = DateTime.UtcNow.Ticks, // generar id numérico único temporalmente
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                DeliveryId = request.DeliveryTypeId,
                OverallStatusId = initialStatusId,
                DeliveryTo = request.Address ?? request.ClientName ?? string.Empty,
                Notes = string.Empty,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0m;

            foreach (var itemReq in request.Items)
            {
                var dish = _dishQuery.GetDish(itemReq.DishId);
                if (dish == null) throw new BadRequestException($"Plato con id {itemReq.DishId} no encontrado.");
                if (!dish.Available) throw new BadRequestException($"El plato {dish.Name} no está disponible.");

                var item = new OrderItem
                {
                    OrderItemId = DateTime.UtcNow.Ticks + (order.OrderItems.Count + 1), // simple generación de id
                    OrderId = order.OrderId,
                    DishId = dish.DishId,
                    Quantity = itemReq.Quantity,
                    StatusId = initialStatusId,
                    Notes = itemReq.Note ?? string.Empty,
                    CreateDate = DateTime.UtcNow
                };

                // PriceAtOrder property might not exist in your model; use Dish.Price to compute total
                total += dish.Price * item.Quantity;

                order.OrderItems.Add(item);
            }

            order.Price = total;

            await _command.InsertOrder(order);

            var saved = _query.GetOrderById(order.OrderId);
            return MapToOrderResponse(saved);
        }

        public async Task<CreateOrderItemResponse> AddOrderItem(long orderId, CreateOrderItemRequest request)
        {
            var order = _query.GetOrderById(orderId);
            if (order == null) throw new NotFoundException("Orden no encontrada");

            var dish = _dishQuery.GetDish(request.DishId);
            if (dish == null) throw new BadRequestException($"Plato con id {request.DishId} no encontrado.");
            if (!dish.Available) throw new BadRequestException($"El plato {dish.Name} no está disponible.");

            var item = new OrderItem
            {
                OrderItemId = DateTime.UtcNow.Ticks,
                OrderId = order.OrderId,
                DishId = dish.DishId,
                Quantity = request.Quantity,
                StatusId = 1,
                Notes = request.Note ?? string.Empty,
                CreateDate = DateTime.UtcNow
            };

            await _command.InsertOrderItem(item);

            // recargar y volver a validar que no sea null
            order = _query.GetOrderById(orderId);
            if (order == null) throw new NotFoundException("Orden no encontrada después de insertar el item");

            // usar Dish? seguro
            order.Price = order.OrderItems.Sum(i => (i.Dish?.Price ?? 0m) * i.Quantity);
            await _command.UpdateOrder(order);

            var added = order.OrderItems.FirstOrDefault(i => i.OrderItemId == item.OrderItemId);
            if (added == null) throw new Exception("El item recién insertado no se encuentra en la orden.");

            return MapToOrderItemResponse(added);
        }
    
        public async Task<CreateOrderItemResponse> UpdateOrderItemStatus(long orderId, long itemId, CreateOrderItemStatusRequest request)
        {
            var order = _query.GetOrderById(orderId);
            if (order == null) throw new NotFoundException("Orden no encontrada");

            var item = order.OrderItems.FirstOrDefault(i => i.OrderItemId == itemId);
            if (item == null) throw new NotFoundException("Item no encontrado");

            item.StatusId = request.StatusId;
            await _command.UpdateOrderItem(item);

            // reevaluar estado global
            var statuses = order.OrderItems.Select(i => i.StatusId).Distinct().ToList();
            if (statuses.Count == 1)
            {
                order.OverallStatusId = statuses.First();
                await _command.UpdateOrder(order);
            }
            else if (statuses.Contains(2))
            {
                order.OverallStatusId = 2;
                await _command.UpdateOrder(order);
            }

            // recargar y validar null
            var updated = _query.GetOrderById(orderId);
            if (updated == null) throw new NotFoundException("Orden no encontrada después de actualizar item");

            var updatedItem = updated.OrderItems.FirstOrDefault(i => i.OrderItemId == itemId);
            if (updatedItem == null) throw new Exception("Item actualizado no encontrado en la orden.");

            return MapToOrderItemResponse(updatedItem);
        }

        public async Task DeleteOrderItem(long orderId, long itemId)
        {
            var order = _query.GetOrderById(orderId);
            if (order == null) throw new NotFoundException("Orden no encontrada");

            var item = order.OrderItems.FirstOrDefault(i => i.OrderItemId == itemId);
            if (item == null) throw new NotFoundException("Item no encontrado");

            await _command.DeleteOrderItem(item);

            // Volvemos a recargar la orden y comprobamos nulos
            order = _query.GetOrderById(orderId);
            if (order == null) throw new NotFoundException("Orden no encontrada después de eliminar el item");

            // Calculamos total con seguridad: (i.Dish?.Price ?? 0m) * i.Quantity
            order.Price = order.OrderItems.Sum(i => (i.Dish?.Price ?? 0m) * i.Quantity);

            await _command.UpdateOrder(order);
        }

        public Task<OrderResponse> GetOrderById(long id)
        {
            var order = _query.GetOrderById(id);
            if (order == null) throw new NotFoundException("Orden no encontrada");
            return Task.FromResult(MapToOrderResponse(order));
        }

        public Task<List<OrderResponse>> GetOrders(DateTime? dateFrom = null, DateTime? dateTo = null, int? statusId = null)
        {
            var orders = _query.GetOrders(dateFrom, dateTo, statusId);
            var result = orders.Select(MapToOrderResponse).ToList();
            return Task.FromResult(result);
        }

        private OrderResponse MapToOrderResponse(Order? order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            var resp = new OrderResponse
            {
                OrderId = order.OrderId,
                Date = order.CreateDate,
                DeliveryTypeId = order.DeliveryId,
                DeliveryTypeName = order.Delivery?.Name ?? string.Empty,
                OverallStatusId = order.OverallStatusId,
                OverallStatusName = order.OverallStatus?.Name ?? string.Empty,
                Total = order.Price
            };
            resp.Items = order.OrderItems.Select(MapToOrderItemResponse).ToList();
            return resp;
        }

        private CreateOrderItemResponse MapToOrderItemResponse(OrderItem i)
        {
            return new CreateOrderItemResponse
            {
                OrderItemId = i.OrderItemId,
                DishId = i.DishId,
                DishName = i.Dish?.Name ?? string.Empty,
                UnitPrice = i.Dish?.Price ?? 0m,
                Quantity = i.Quantity,
                StatusId = i.StatusId,
                StatusName = i.Status?.Name ?? string.Empty,
                Note = i.Notes
            };
        }

        public async Task<bool> UpdateOrderStatus(long orderId, int newStatusId)
        {
            // Traer la orden actual desde la capa de consultas
            var order = _query.GetOrderById(orderId);

            if (order == null)
                throw new Exception($"No se encontró la orden con ID {orderId}");

            // Validación: solo se puede avanzar de uno en uno
            if (newStatusId != order.OverallStatusId + 1)
                throw new Exception($"No se puede pasar del estado {order.OverallStatusId} al {newStatusId}. Solo se puede avanzar uno por vez.");

            // Delegar la actualización al comando
            await _command.UpdateOrderStatus(orderId, newStatusId);
            return true;
        }

    }
}