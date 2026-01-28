using FreightQuotation.API.DTOs;

namespace FreightQuotation.API.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendQuotationEmailAsync(string toEmail, string supplierName, string quotationNumber, 
            List<QuotationDetailResponseDto> items, decimal totalAmount, string token);
    }
}