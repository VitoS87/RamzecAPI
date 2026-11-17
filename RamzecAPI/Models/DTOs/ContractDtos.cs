namespace RamzecAPI.DTOs
{
    public record ContractCreateDto(string ContractNumber, int CustomerId, decimal TotalAmount);
    public record ContractUpdateDto(string ContractNumber, decimal TotalAmount, string Status);
}