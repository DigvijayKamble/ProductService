namespace ProductService.Domain.Entities;

public class ProductInventory
{
    public Guid ProductId { get; private set; }
    public Guid WarehouseId { get; private set; }
    public int StockQuantity { get; private set; }

    internal ProductInventory(Guid productId, Guid warehouseId, int quantity)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
        StockQuantity = quantity;
    }

    public void IncreaseStock(int qty)
    {
        StockQuantity += qty;
    }

    public void DecreaseStock(int qty)
    {
        if (StockQuantity < qty)
            throw new InvalidOperationException("Insufficient stock.");

        StockQuantity -= qty;
    }
}
