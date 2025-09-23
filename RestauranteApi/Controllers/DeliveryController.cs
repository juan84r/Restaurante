using Aplication.Interfaces;
using Aplication.Services;
using Aplication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryServices _services;

        public DeliveryController(IDeliveryServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAll();
            return new JsonResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var result = await _services.GetById(id);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery(CreateDeliveryRequest request)
        {
            var result = await _services.CreateDelivery(request);
            return new JsonResult(result) {StatusCode = 201}; 
        }
    }
}
