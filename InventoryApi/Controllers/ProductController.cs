using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Data;
using InventoryApi.DTOs;
using InventoryApi.Models;

namespace InventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/products (Con filtros LINQ avanzadas)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(
        [FromQuery] string? search,
        [FromQuery] int? categoryId,
        [FromQuery] bool? lowStockOnly)
    {
        var query = _context.Products.Include(p => p.Category).AsQueryable();

        // Filtro por búsqueda de texto
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search) || p.SKU.Contains(search));
        }

        // Filtro por categoría
        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        // Filtro por alerta de Stock Bajo (Stock < Umbral)
        if (lowStockOnly.HasValue && lowStockOnly.Value)
        {
            query = query.Where(p => p.StockQuantity <= p.MinimumStockThreshold);
        }

        var products = await query.Select(p => new ProductDto
        {
            Id = p.Id,
            SKU = p.SKU,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            MinimumStockThreshold = p.MinimumStockThreshold,
            LowStockAlert = p.StockQuantity <= p.MinimumStockThreshold,
            CategoryName = p.Category != null ? p.Category.Name : "Sin Categoría"
        }).ToListAsync();

        return Ok(products);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto dto)
    {
        if (await _context.Products.AnyAsync(p => p.SKU == dto.SKU))
        {
            return BadRequest(new { message = "Ya existe un producto registrado con este código SKU." });
        }

        var product = new Product
        {
            SKU = dto.SKU,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            MinimumStockThreshold = dto.MinimumStockThreshold,
            CategoryId = dto.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, dto);
    }

    // GET: api/products/low-stock (Endpoint rápido para dashboard de alertas)
    [HttpGet("low-stock")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetLowStockProducts()
    {
        var lowStockProducts = await _context.Products
            .Include(p => p.Category)
            .Where(p => p.StockQuantity <= p.MinimumStockThreshold)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                MinimumStockThreshold = p.MinimumStockThreshold,
                LowStockAlert = true,
                CategoryName = p.Category != null ? p.Category.Name : "Sin Categoría"
            }).ToListAsync();

        return Ok(lowStockProducts);
    }
}