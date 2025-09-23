using Aplication.Interfaces;
using Aplication.Services;
using Aplication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aplication.Exceptions;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _services.GetById(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message =  ex.Message});
            }
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateDish([FromRoute] Guid id, [FromBody] CreateDishRequest request)
        {
            /*var updateDish = await _services.UpdateDish(id, request);
            if (updateDish == null) return NotFound();
            return Ok(updateDish);*/
             try
            {
                var result = await _services.UpdateDish(id, request);
                return Ok(result);
            }
            catch (NotFoundException ex) // Captura la excepción del servicio
            {
                // Devuelve 404 con el mensaje de la excepción
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDish(CreateDishRequest request)
        {
            /*(No)var result = await _services.CreateDish(request);
            return CreatedAtAction(nameof(GetById), new {id = result.DishId}, result);*/
            try
            {
                var result = await _services.CreateDish(request);
                return Ok(result);
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Esto sigue siendo un 500 para errores inesperados
                return StatusCode(500, new { message = ex.Message });
            }      
           
         
        }
    }
}
