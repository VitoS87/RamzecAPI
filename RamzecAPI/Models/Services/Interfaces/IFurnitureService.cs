using RamzecAPI.DTOs;
using RamzecAPI.Models;

namespace RamzecAPI.Services.Interfaces
{
    public interface IFurnitureService
    {
        Task<IEnumerable<Furniture>> GetAllAsync();
        Task<Furniture?> GetByIdAsync(int id);
        Task<Furniture> CreateAsync(FurnitureCreateUpdateDto dto);
        Task<bool> UpdateAsync(int id, FurnitureCreateUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}