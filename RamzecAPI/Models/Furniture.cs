using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RamzecAPI.Models
{
    [Table("furniture_models")]
    public class Furniture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Category { get; set; } = string.Empty; // Например: Диван, Стол, Шкаф

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? Material { get; set; }

        public int StockQuantity { get; set; }
    }
}