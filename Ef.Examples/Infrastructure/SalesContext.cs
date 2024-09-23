using Ef.Examples.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ef.Examples.Infrastructure;

public class SalesContext(IConfiguration configuration) : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }

    private readonly IConfiguration _configuration = configuration;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(p =>
        {
            p.ToTable(nameof(Order));
            p.HasKey(c => c.Id);
            p.HasMany(c => c.Items);
        });

        modelBuilder.Entity<OrderItem>(p =>
        {
            p.ToTable(nameof(OrderItem));
            p.HasKey(c => c.Id);
            p.HasOne(c => c.Product);
        });

        modelBuilder.Entity<Product>(p =>
        {
            p.ToTable(nameof(Product));
            p.HasKey(c => c.Id);
        });

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SalesContext"));
}