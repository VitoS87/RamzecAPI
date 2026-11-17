namespace RamzecAPI.DTOs
{
    public record SaleFactCreateDto(int ContractId, int FurnitureId, int Quantity, decimal? UnitPrice);
}