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
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAll();
            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var result = await _services.GetById(id);
            return new JsonResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateDishRequest request)
        {
            var result = await _services.CreateDish(request);
            return new JsonResult(result) {StatusCode = 201};
        }
    }
}
