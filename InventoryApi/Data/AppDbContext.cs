using Microsoft.EntityFrameworkCore;
using InventoryApi.Models;

namespace InventoryApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Sistemas y Servidores", Description = "Componentes de infraestructura IT" },
            new Category { Id = 2, Name = "Periféricos", Description = "Teclados, ratones, impresoras de tickets" }
        );

        // Usar una fecha FIJA estática para evitar valores dinámicos
        var staticDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Product>().HasData(
            new Product 
            { 
                Id = 1, 
                SKU = "SERV-001", 
                Name = "Servidor Rack 1U", 
                Description = "Servidor para virtualización", 
                Price = 1200.00m, 
                StockQuantity = 2, 
                MinimumStockThreshold = 3,
                CreatedAt = staticDate, // <-- FECHA FIJA
                CategoryId = 1 
            },
            new Product 
            { 
                Id = 2, 
                SKU = "PRIN-002", 
                Name = "Impresora Térmica TPV", 
                Description = "Impresora de recibos USB/Ethernet", 
                Price = 145.50m, 
                StockQuantity = 12, 
                MinimumStockThreshold = 5, 
                CreatedAt = staticDate, // <-- FECHA FIJA
                CategoryId = 2 
            }
        );
    }
}