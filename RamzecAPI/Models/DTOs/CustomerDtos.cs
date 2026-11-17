namespace RamzecAPI.DTOs
{
    public record CustomerCreateUpdateDto(string FullName, string Phone, string? Email, string? Address);
}