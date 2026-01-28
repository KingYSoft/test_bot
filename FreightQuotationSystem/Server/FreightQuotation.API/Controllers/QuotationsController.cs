using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreightQuotation.API.Data;
using FreightQuotation.API.Models;
using FreightQuotation.API.DTOs;
using FreightQuotation.API.Services;
using FreightQuotation.API.Services.Interfaces;

namespace FreightQuotation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public QuotationsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationResponseDto>>> GetQuotations()
        {
            var quotations = await _context.Quotations
                .Include(q => q.Supplier)
                .Include(q => q.Details)
                .OrderByDescending(q => q.CreatedAt)
                .Select(q => new QuotationResponseDto
                {
                    Id = q.Id,
                    QuotationNumber = q.QuotationNumber,
                    SupplierName = q.Supplier.Name,
                    SupplierEmail = q.Supplier.Email,
                    TotalAmount = q.TotalAmount,
                    Status = q.Status,
                    CreatedAt = q.CreatedAt,
                    Details = q.Details.Select(d => new QuotationDetailResponseDto
                    {
                        Id = d.Id,
                        ItemDescription = d.ItemDescription,
                        ServiceType = d.ServiceType,
                        Origin = d.Origin,
                        Destination = d.Destination,
                        WeightOrVolume = d.WeightOrVolume,
                        UnitPrice = d.UnitPrice,
                        Amount = d.Amount,
                        CreatedAt = d.CreatedAt
                    }).ToList()
                })
                .ToListAsync();

            return Ok(quotations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationResponseDto>> GetQuotation(int id)
        {
            var quotation = await _context.Quotations
                .Include(q => q.Supplier)
                .Include(q => q.Details)
                .Where(q => q.Id == id)
                .Select(q => new QuotationResponseDto
                {
                    Id = q.Id,
                    QuotationNumber = q.QuotationNumber,
                    SupplierName = q.Supplier.Name,
                    SupplierEmail = q.Supplier.Email,
                    TotalAmount = q.TotalAmount,
                    Status = q.Status,
                    CreatedAt = q.CreatedAt,
                    Details = q.Details.Select(d => new QuotationDetailResponseDto
                    {
                        Id = d.Id,
                        ItemDescription = d.ItemDescription,
                        ServiceType = d.ServiceType,
                        Origin = d.Origin,
                        Destination = d.Destination,
                        WeightOrVolume = d.WeightOrVolume,
                        UnitPrice = d.UnitPrice,
                        Amount = d.Amount,
                        CreatedAt = d.CreatedAt
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (quotation == null)
            {
                return NotFound();
            }

            return Ok(quotation);
        }

        [HttpPost]
        public async Task<ActionResult<QuotationResponseDto>> CreateQuotation(QuotationCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 计算总金额
                var totalAmount = dto.Details.Sum(d => d.Amount);
                
                // 生成报价单号
                var quotationNumber = $"QT-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";
                
                // 创建报价单
                var quotation = new Quotation
                {
                    QuotationNumber = quotationNumber,
                    SupplierId = dto.SupplierId,
                    TotalAmount = totalAmount,
                    Status = "draft"
                };

                _context.Quotations.Add(quotation);
                await _context.SaveChangesAsync();

                // 添加明细
                foreach (var detailDto in dto.Details)
                {
                    var detail = new QuotationDetail
                    {
                        QuotationId = quotation.Id,
                        ItemDescription = detailDto.ItemDescription,
                        ServiceType = detailDto.ServiceType,
                        Origin = detailDto.Origin,
                        Destination = detailDto.Destination,
                        WeightOrVolume = detailDto.WeightOrVolume,
                        UnitPrice = detailDto.UnitPrice,
                        Amount = detailDto.Amount
                    };
                    _context.QuotationDetails.Add(detail);
                }

                await _context.SaveChangesAsync();

                // 获取完整报价单信息
                var response = await _context.Quotations
                    .Include(q => q.Supplier)
                    .Include(q => q.Details)
                    .Where(q => q.Id == quotation.Id)
                    .Select(q => new QuotationResponseDto
                    {
                        Id = q.Id,
                        QuotationNumber = q.QuotationNumber,
                        SupplierName = q.Supplier.Name,
                        SupplierEmail = q.Supplier.Email,
                        TotalAmount = q.TotalAmount,
                        Status = q.Status,
                        CreatedAt = q.CreatedAt,
                        Details = q.Details.Select(d => new QuotationDetailResponseDto
                        {
                            Id = d.Id,
                            ItemDescription = d.ItemDescription,
                            ServiceType = d.ServiceType,
                            Origin = d.Origin,
                            Destination = d.Destination,
                            WeightOrVolume = d.WeightOrVolume,
                            UnitPrice = d.UnitPrice,
                            Amount = d.Amount,
                            CreatedAt = d.CreatedAt
                        }).ToList()
                    })
                    .FirstAsync();

                await transaction.CommitAsync();
                
                // 发送邮件
                var token = await GenerateConfirmationToken(quotation.Id);
                var emailSent = await _emailService.SendQuotationEmailAsync(
                    response.SupplierEmail,
                    response.SupplierName,
                    response.QuotationNumber,
                    response.Details,
                    response.TotalAmount,
                    token
                );

                if (emailSent)
                {
                    // 更新状态为已发送
                    quotation.Status = "sent";
                    await _context.SaveChangesAsync();
                    
                    response.Status = "sent";
                }

                return CreatedAtAction(nameof(GetQuotation), new { id = quotation.Id }, response);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<string> GenerateConfirmationToken(int quotationId)
        {
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var expiryTime = DateTime.UtcNow.AddHours(24); // 24小时过期
            
            var confirmationToken = new ConfirmationToken
            {
                QuotationId = quotationId,
                Token = token,
                ExpiryTime = expiryTime
            };
            
            _context.ConfirmationTokens.Add(confirmationToken);
            await _context.SaveChangesAsync();
            
            return token;
        }
    }
}