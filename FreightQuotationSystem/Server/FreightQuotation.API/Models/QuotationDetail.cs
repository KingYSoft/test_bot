using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreightQuotation.API.Models
{
    public class QuotationDetail
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int QuotationId { get; set; }
        
        [StringLength(500)]
        public string ItemDescription { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string ServiceType { get; set; } = string.Empty; // air_freight, sea_freight, land_transport, customs_clearance, insurance
        
        [StringLength(255)]
        public string Origin { get; set; } = string.Empty;
        
        [StringLength(255)]
        public string Destination { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal WeightOrVolume { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [ForeignKey(nameof(QuotationId))]
        public virtual Quotation Quotation { get; set; } = null!;
    }
}