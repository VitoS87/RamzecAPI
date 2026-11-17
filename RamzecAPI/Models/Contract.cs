using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RamzecAPI.Models
{
    [Table("contracts")]
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContractNumber { get; set; } = string.Empty;

        public DateTime DateSigned { get; set; } = DateTime.UtcNow;

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Заключен"; // Заключен, Исполнен, Расторгнут
    }
}
