namespace RamzecAPI.DTOs
{
    public record FurnitureCreateUpdateDto(string Name, string Category, decimal Price, string? Material, int StockQuantity);
}
