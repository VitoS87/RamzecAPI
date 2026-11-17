using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RamzecAPI.Models
{
    [Table("sale_facts")]
    public class SaleFact
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Contract")]
        public int ContractId { get; set; }
        public Contract? Contract { get; set; }

        [ForeignKey("Furniture")]
        public int FurnitureId { get; set; }
        public Furniture? Furniture { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    }
}
