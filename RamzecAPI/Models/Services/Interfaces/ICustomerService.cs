using RamzecAPI.DTOs;
using RamzecAPI.Models;

namespace RamzecAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<Customer> CreateAsync(CustomerCreateUpdateDto dto);
        Task<bool> UpdateAsync(int id, CustomerCreateUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}