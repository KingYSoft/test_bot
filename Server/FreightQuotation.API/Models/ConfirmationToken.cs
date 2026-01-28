using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreightQuotation.API.Models
{
    public class ConfirmationToken
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int QuotationId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Token { get; set; } = string.Empty;
        
        public DateTime ExpiryTime { get; set; }
        
        public bool Used { get; set; } = false;
        
        [StringLength(20)]
        public string? Action { get; set; } // confirm, reject
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [ForeignKey(nameof(QuotationId))]
        public virtual Quotation Quotation { get; set; } = null!;
    }
}