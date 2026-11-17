using RamzecAPI.DTOs;
using RamzecAPI.Models;

namespace RamzecAPI.Services.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleFact>> GetAllAsync();
        Task<SaleFact?> GetByIdAsync(int id);
        Task<SaleFact> RegisterSaleAsync(SaleFactCreateDto dto);
        Task<bool> CancelSaleAsync(int id);
    }
}