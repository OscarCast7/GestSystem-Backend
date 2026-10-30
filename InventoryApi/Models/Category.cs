namespace InventoryApi.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Relación: Una categoría tiene muchos productos
    public ICollection<Product> Products { get; set; } = new List<Product>();
}