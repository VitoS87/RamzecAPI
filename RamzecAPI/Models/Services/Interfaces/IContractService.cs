using RamzecAPI.DTOs;
using RamzecAPI.Models;

namespace RamzecAPI.Services.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<Contract>> GetAllAsync();
        Task<Contract?> GetByIdAsync(int id);
        Task<Contract> CreateAsync(ContractCreateDto dto);
        Task<bool> UpdateAsync(int id, ContractUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}