using Aplication.Interfaces;
using Aplication.UseCase.Restaurante.GetAll;
using Aplication.UseCase.Restaurante.Create.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishServices _services;

        public DishController(IDishServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? name, [FromQuery] int? categoryId, [FromQuery] string? order)
        {
            var result = await _services.GetAll(name, categoryId, order);
            return new JsonResult(result);
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateDish([FromRoute] Guid id, [FromBody] CreateDishRequest request)
        {
            var updateDish = await _services.UpdateDish(id, request);
            if (updateDish == null) return NotFound();
            return Ok(updateDish);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateDishRequest request)
        {
            var result = await _services.CreateDish(request);
            return new JsonResult(result) {StatusCode = 201};
        }
    }
}
