using Microsoft.AspNetCore.Mvc;
using Aplication.UseCase.Restaurante.Create.Models;
using Aplication.UseCase.Restaurante.Update.Models;
using Aplication.Response;
using Aplication.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public class DishController : ControllerBase
{
    private readonly IDishCommand _dishCommand;
    private readonly IDishQuery _dishQuery;

    public DishController(IDishCommand dishCommand, IDishQuery dishQuery)
    {
        _dishCommand = dishCommand;
        _dishQuery = dishQuery;
    }

    // POST: api/v1/dish
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDishRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // check unique name
        var exists = await _dishQuery.ExistsByNameAsync(request.Name);
        if (exists) return Conflict(new { message = "Ya existe un plato con ese nombre" });

        var created = await _dishCommand.CreateDishAsync(request);

        // map to response or return created object
        return CreatedAtAction(nameof(GetById), new { id = created.DishId }, created);
    }

    // GET: api/v1/dish
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? name = null, [FromQuery] int? categoryId = null, [FromQuery] string? sortByPrice = null)
    {
        var list = await _dishQuery.GetAllAsync(name, categoryId, sortByPrice);
        return Ok(list);
    }

    // GET by id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dish = await _dishQuery.GetByIdAsync(id);
        if (dish == null) return NotFound();
        return Ok(dish);
    }

    // PUT: api/v1/dish/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDishRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != request.DishId) return BadRequest(new { message = "El id de la ruta y del body deben coincidir" });

        var existing = await _dishQuery.GetByIdAsync(id);
        if (existing == null) return NotFound();

        // comprobar nombre único (si cambiás el nombre y ya existe en otro id)
        if (!string.Equals(existing.Name, request.Name, StringComparison.OrdinalIgnoreCase))
        {
            var nameUsed = await _dishQuery.ExistsByNameAsync(request.Name);
            if (nameUsed) return Conflict(new { message = "Ya existe un plato con ese nombre" });
        }

        var updated = await _dishCommand.UpdateDishAsync(request);
        return Ok(updated);
    }
}
