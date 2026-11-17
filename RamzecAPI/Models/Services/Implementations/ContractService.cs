using Microsoft.EntityFrameworkCore;
using RamzecAPI.Data;
using RamzecAPI.DTOs;
using RamzecAPI.Models;
using RamzecAPI.Services.Interfaces;

namespace RamzecAPI.Services.Implementations
{
    public class ContractService : IContractService
    {
        private readonly AppDbContext _context;

        public ContractService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contract>> GetAllAsync() =>
            await _context.Contracts.Include(c => c.Customer).AsNoTracking().ToListAsync();

        public async Task<Contract?> GetByIdAsync(int id) =>
            await _context.Contracts.Include(c => c.Customer).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Contract> CreateAsync(ContractCreateDto dto)
        {
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId);
            if (!customerExists)
                throw new ArgumentException($"Покупатель с ID {dto.CustomerId} не найден.");

            var existsNumber = await _context.Contracts.AnyAsync(c => c.ContractNumber == dto.ContractNumber);
            if (existsNumber)
                throw new InvalidOperationException($"Договор с номером '{dto.ContractNumber}' уже существует.");

            var contract = new Contract
            {
                ContractNumber = dto.ContractNumber,
                CustomerId = dto.CustomerId,
                TotalAmount = dto.TotalAmount,
                DateSigned = DateTime.UtcNow,
                Status = "Заключен"
            };

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<bool> UpdateAsync(int id, ContractUpdateDto dto)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return false;

            contract.ContractNumber = dto.ContractNumber;
            contract.TotalAmount = dto.TotalAmount;
            contract.Status = dto.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return false;

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}