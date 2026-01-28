using Microsoft.EntityFrameworkCore;
using FreightQuotation.API.Models;

namespace FreightQuotation.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationDetail> QuotationDetails { get; set; }
        public DbSet<ConfirmationToken> ConfirmationTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure indexes
            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Quotation>()
                .HasIndex(q => q.QuotationNumber)
                .IsUnique();

            modelBuilder.Entity<ConfirmationToken>()
                .HasIndex(ct => ct.Token)
                .IsUnique();

            // Seed initial data
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "全球物流有限公司", ContactPerson = "张经理", Email = "contact@globalexpress.com", Phone = "13800138000", Address = "北京市朝阳区" },
                new Supplier { Id = 2, Name = "海运通国际", ContactPerson = "李总监", Email = "info@oceanfreight.com", Phone = "13900139000", Address = "上海市浦东新区" }
            );
        }
    }
}