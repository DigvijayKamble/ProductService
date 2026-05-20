namespace ProductService.Domain.Entities;

public class ProductInventory
{
    public int InventoryId { get; set; }
    public int ProductId { get; set; }
    public int StockQuantity { get; set; } 
    public Product Product { get; set; } = null!;
}