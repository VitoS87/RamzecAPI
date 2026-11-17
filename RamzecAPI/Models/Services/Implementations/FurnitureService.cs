using Microsoft.EntityFrameworkCore;
using RamzecAPI.Data;
using RamzecAPI.DTOs;
using RamzecAPI.Models;
using RamzecAPI.Services.Interfaces;

namespace RamzecAPI.Services.Implementations
{
    public class FurnitureService : IFurnitureService
    {
        private readonly AppDbContext _context;

        public FurnitureService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Furniture>> GetAllAsync() =>
            await _context.Furnitures.AsNoTracking().ToListAsync();

        public async Task<Furniture?> GetByIdAsync(int id) =>
            await _context.Furnitures.FindAsync(id);

        public async Task<Furniture> CreateAsync(FurnitureCreateUpdateDto dto)
        {
            var item = new Furniture
            {
                Name = dto.Name,
                Category = dto.Category,
                Price = dto.Price,
                Material = dto.Material,
                StockQuantity = dto.StockQuantity
            };

            _context.Furnitures.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> UpdateAsync(int id, FurnitureCreateUpdateDto dto)
        {
            var item = await _context.Furnitures.FindAsync(id);
            if (item == null) return false;

            item.Name = dto.Name;
            item.Category = dto.Category;
            item.Price = dto.Price;
            item.Material = dto.Material;
            item.StockQuantity = dto.StockQuantity;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Furnitures.FindAsync(id);
            if (item == null) return false;

            _context.Furnitures.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}