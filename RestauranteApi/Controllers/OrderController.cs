using Microsoft.AspNetCore.Mvc;
using Aplication.Interfaces;
using System;
using System.Threading.Tasks;
using Aplication;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _services;
        public OrderController(IOrderServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await _services.CreateOrder(request);
            return CreatedAtAction(nameof(GetById), new { id = result.OrderId }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo, [FromQuery] int? statusId)
        {
            var list = await _services.GetOrders(dateFrom, dateTo, statusId);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var order = await _services.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddItem(long orderId, [FromBody] CreateOrderItemRequest request)
        {
            var item = await _services.AddOrderItem(orderId, request);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, item);
        }

        [HttpPut("{orderId}/items/{itemId}/status")]
        public async Task<IActionResult> UpdateItemStatus(long orderId, long itemId, [FromBody] CreateOrderItemStatusRequest request)
        {
            var updated = await _services.UpdateOrderItemStatus(orderId, itemId, request);
            return Ok(updated);
        }

        [HttpDelete("{orderId}/items/{itemId}")]
        public async Task<IActionResult> DeleteItem(long orderId, long itemId)
        {
            await _services.DeleteOrderItem(orderId, itemId);
            return NoContent();
        }
    }
}
