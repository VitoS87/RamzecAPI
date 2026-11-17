using Microsoft.AspNetCore.Mvc;
using RamzecAPI.DTOs;
using RamzecAPI.Models;
using RamzecAPI.Services.Interfaces;

namespace RamzecAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleFact>>> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleFact>> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null)
            {
                return NotFound(new { message = $"Запись продажи с ID {id} не найдена." });
            }

            return Ok(sale);
        }

        // POST: api/Sales
        [HttpPost]
        public async Task<ActionResult<SaleFact>> Register([FromBody] SaleFactCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = await _saleService.RegisterSaleAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var success = await _saleService.CancelSaleAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Запись продажи с ID {id} не найдена." });
            }

            return NoContent();
        }
    }
}