using Microsoft.AspNetCore.Mvc;
using RamzecAPI.DTOs;
using RamzecAPI.Models;
using RamzecAPI.Services.Interfaces;

namespace RamzecAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        // GET: api/Contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetAll()
        {
            var contracts = await _contractService.GetAllAsync();
            return Ok(contracts);
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetById(int id)
        {
            var contract = await _contractService.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound(new { message = $"Договор с ID {id} не найден." });
            }

            return Ok(contract);
        }

        // POST: api/Contracts
        [HttpPost]
        public async Task<ActionResult<Contract>> Create([FromBody] ContractCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdContract = await _contractService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdContract.Id }, createdContract);
            }
            catch (ArgumentException ex)
            {
                // Например: покупатель не найден
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Например: номер договора уже занят
                return Conflict(new { message = ex.Message });
            }
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContractUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _contractService.UpdateAsync(id, dto);
            if (!updated)
            {
                return NotFound(new { message = $"Договор с ID {id} не найден для обновления." });
            }

            return NoContent();
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _contractService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Договор с ID {id} не найден." });
            }

            return NoContent();
        }
    }
}