namespace InventoryApi.Models;

public class Product
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    
    public int StockQuantity { get; set; }
    public int MinimumStockThreshold { get; set; } = 5;
    
    // Dejar la propiedad simple sin asignación dinámica por defecto
    public DateTime CreatedAt { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}