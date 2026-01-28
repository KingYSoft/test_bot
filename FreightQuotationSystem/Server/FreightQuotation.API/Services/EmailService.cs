using FreightQuotation.API.DTOs;
using FreightQuotation.API.Services.Interfaces;
using System.Text;

namespace FreightQuotation.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendQuotationEmailAsync(string toEmail, string supplierName, string quotationNumber, 
            List<QuotationDetailResponseDto> items, decimal totalAmount, string token)
        {
            try
            {
                var baseUrl = _configuration["BaseUrl"];
                var confirmationLink = $"{baseUrl}/confirm?token={token}&action=confirm";
                var rejectionLink = $"{baseUrl}/confirm?token={token}&action=reject";

                var sb = new StringBuilder();
                sb.AppendLine("<html><body>");
                sb.AppendLine($"<h2>报价单请求</h2>");
                sb.AppendLine($"<p>尊敬的 {supplierName},</p>");
                sb.AppendLine($"<p>您收到了一份新的报价单请求，编号: <strong>{quotationNumber}</strong></p>");
                sb.AppendLine("<h3>报价明细:</h3>");
                sb.AppendLine("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; width: 100%;'>");
                sb.AppendLine("<thead><tr>");
                sb.AppendLine("<th style='padding: 8px; text-align: left;'>服务描述</th>");
                sb.AppendLine("<th style='padding: 8px; text-align: left;'>服务类型</th>");
                sb.AppendLine("<th style='padding: 8px; text-align: left;'>起始地</th>");
                sb.AppendLine("<th style='padding: 8px; text-align: left;'>目的地</th>");
                sb.AppendLine("<th style='padding: 8px; text-align: left;'>重量/体积</th>");
                sb.AppendLine("<th style='padding: 8px; text-align: left;'>单价</th>");
                sb.AppendLine("<th style='padding: 8px; text-align: left;'>金额</th>");
                sb.AppendLine("</tr></thead><tbody>");

                foreach (var item in items)
                {
                    sb.AppendLine("<tr>");
                    sb.AppendLine($"<td style='padding: 8px;'>{item.ItemDescription}</td>");
                    sb.AppendLine($"<td style='padding: 8px;'>{item.ServiceType}</td>");
                    sb.AppendLine($"<td style='padding: 8px;'>{item.Origin}</td>");
                    sb.AppendLine($"<td style='padding: 8px;'>{item.Destination}</td>");
                    sb.AppendLine($"<td style='padding: 8px;'>{item.WeightOrVolume}</td>");
                    sb.AppendLine($"<td style='padding: 8px;'>{item.UnitPrice}</td>");
                    sb.AppendLine($"<td style='padding: 8px;'>{item.Amount}</td>");
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</tbody><tfoot><tr>");
                sb.AppendLine("<td colspan='6' style='padding: 8px; text-align: right;'><strong>总计</strong></td>");
                sb.AppendLine($"<td style='padding: 8px; text-align: left;'><strong>{totalAmount}</strong></td>");
                sb.AppendLine("</tr></tfoot></table>");

                sb.AppendLine("<div style='margin-top: 20px; padding: 15px; background-color: #f9f9f9; border-radius: 5px;'>");
                sb.AppendLine("<p><strong>请点击以下链接确认或拒绝此报价单：</strong></p>");
                sb.AppendLine($"<p><a href='{confirmationLink}' style='display: inline-block; padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none; border-radius: 5px; margin-right: 10px;'>确认报价</a>");
                sb.AppendLine($"<a href='{rejectionLink}' style='display: inline-block; padding: 10px 20px; background-color: #f44336; color: white; text-decoration: none; border-radius: 5px;'>拒绝报价</a></p>");
                sb.AppendLine("<p style='color: #666; font-size: 12px;'>请注意：此链接将在24小时内过期。</p>");
                sb.AppendLine("</div>");

                sb.AppendLine("</body></html>");

                // 这里应该使用实际的邮件发送服务
                // 例如：SmtpClient, SendGrid, MailKit 等
                Console.WriteLine($"邮件将发送至: {toEmail}");
                Console.WriteLine($"邮件内容: {sb.ToString()}");

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送邮件失败: {ex.Message}");
                return await Task.FromResult(false);
            }
        }
    }
}