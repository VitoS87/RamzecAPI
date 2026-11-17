using Microsoft.EntityFrameworkCore;
using RamzecAPI.Data;
using RamzecAPI.DTOs;
using RamzecAPI.Models;
using RamzecAPI.Services.Interfaces;

namespace RamzecAPI.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;

        public SaleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SaleFact>> GetAllAsync() =>
            await _context.SaleFacts
                .Include(s => s.Contract)
                .Include(s => s.Furniture)
                .AsNoTracking()
                .ToListAsync();

        public async Task<SaleFact?> GetByIdAsync(int id) =>
            await _context.SaleFacts
                .Include(s => s.Contract)
                .Include(s => s.Furniture)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<SaleFact> RegisterSaleAsync(SaleFactCreateDto dto)
        {
            if (dto.Quantity <= 0)
                throw new ArgumentException("Количество должно быть больше нуля.");

            // Используем транзакцию, чтобы гарантировать целостность данных при списании
            using var transaction = await _context.Database.BeginTransactionAsync();

            var contract = await _context.Contracts.FindAsync(dto.ContractId);
            if (contract == null)
                throw new KeyNotFoundException($"Договор с ID {dto.ContractId} не найден.");

            var furniture = await _context.Furnitures.FindAsync(dto.FurnitureId);
            if (furniture == null)
                throw new KeyNotFoundException($"Модель мебели с ID {dto.FurnitureId} не найдена.");

            if (furniture.StockQuantity < dto.Quantity)
                throw new InvalidOperationException($"Недостаточно остатков на складе. В наличии: {furniture.StockQuantity}, запрошено: {dto.Quantity}.");

            // 1. Списание со склада
            furniture.StockQuantity -= dto.Quantity;

            // 2. Определение итоговой стоимости за единицу
            decimal finalPrice = dto.UnitPrice.HasValue && dto.UnitPrice.Value > 0
                ? dto.UnitPrice.Value
                : furniture.Price;

            var saleFact = new SaleFact
            {
                ContractId = dto.ContractId,
                FurnitureId = dto.FurnitureId,
                Quantity = dto.Quantity,
                UnitPrice = finalPrice,
                SaleDate = DateTime.UtcNow
            };

            _context.SaleFacts.Add(saleFact);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return saleFact;
        }

        public async Task<bool> CancelSaleAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var sale = await _context.SaleFacts.FindAsync(id);
            if (sale == null) return false;

            // Возврат товара на склад при отмене продажи
            var furniture = await _context.Furnitures.FindAsync(sale.FurnitureId);
            if (furniture != null)
            {
                furniture.StockQuantity += sale.Quantity;
            }

            _context.SaleFacts.Remove(sale);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return true;
        }
    }
}