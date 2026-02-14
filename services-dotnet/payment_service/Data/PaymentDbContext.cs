using Microsoft.EntityFrameworkCore;
using payment_service.Models;

namespace payment_service.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // You can add extra constraints here if needed
        modelBuilder.Entity<Transaction>().Property(t => t.Status).HasMaxLength(20);
    }
}