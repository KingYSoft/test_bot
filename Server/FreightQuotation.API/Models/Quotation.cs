using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreightQuotation.API.Models
{
    public class Quotation
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string QuotationNumber { get; set; } = string.Empty;
        
        [Required]
        public int SupplierId { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }
        
        [StringLength(20)]
        public string Status { get; set; } = "draft"; // draft, sent, confirmed, rejected, expired
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        [ForeignKey(nameof(SupplierId))]
        public virtual Supplier Supplier { get; set; } = null!;
        
        public virtual ICollection<QuotationDetail> Details { get; set; } = new List<QuotationDetail>();
    }
}