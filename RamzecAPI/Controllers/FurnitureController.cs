using Microsoft.AspNetCore.Mvc;
using RamzecAPI.DTOs;
using RamzecAPI.Models;
using RamzecAPI.Services.Interfaces;

namespace RamzecAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FurnitureController : ControllerBase
    {
        private readonly IFurnitureService _service;

        public FurnitureController(IFurnitureService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Furniture>>> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Furniture>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Furniture>> Create([FromBody] FurnitureCreateUpdateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FurnitureCreateUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}