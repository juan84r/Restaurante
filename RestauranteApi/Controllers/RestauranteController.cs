using Aplication.Interfaces;
using Aplication.UseCase.Restaurante.GetAll;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly ICategoryServices _services;

        public RestauranteController(ICategoryServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAll();
            return new JsonResult(result);
        }
        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            var result = await _services.CreateCategory(request);
            return new JsonResult(result);
        }
    }
}
