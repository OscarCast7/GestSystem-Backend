namespace InventoryApi.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int MinimumStockThreshold { get; set; }
    public bool LowStockAlert { get; set; } // Propiedad calculada
    public string CategoryName { get; set; } = string.Empty;
}