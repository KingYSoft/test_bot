using System.ComponentModel.DataAnnotations;

namespace FreightQuotation.API.DTOs
{
    public class QuotationCreateDto
    {
        [Required]
        public int SupplierId { get; set; }
        
        [Required]
        public List<QuotationDetailCreateDto> Details { get; set; } = new List<QuotationDetailCreateDto>();
    }

    public class QuotationDetailCreateDto
    {
        [Required]
        [StringLength(500)]
        public string ItemDescription { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string ServiceType { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string Origin { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string Destination { get; set; } = string.Empty;
        
        [Required]
        public decimal WeightOrVolume { get; set; }
        
        [Required]
        public decimal UnitPrice { get; set; }
        
        public decimal Amount { get; set; }
    }

    public class QuotationResponseDto
    {
        public int Id { get; set; }
        public string QuotationNumber { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string SupplierEmail { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<QuotationDetailResponseDto> Details { get; set; } = new List<QuotationDetailResponseDto>();
    }

    public class QuotationDetailResponseDto
    {
        public int Id { get; set; }
        public string ItemDescription { get; set; } = string.Empty;
        public string ServiceType { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public decimal WeightOrVolume { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}