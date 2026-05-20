namespace ProductService.Domain.Entities;

public class Inventory
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int QuantityAvailable { get; private set; }
    public int? ReorderLevel { get; private set; }
    public DateTime LastUpdated { get; private set; }

    private Inventory() { }

    public Inventory(Guid productId, int initialStock)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        QuantityAvailable = initialStock;
        LastUpdated = DateTime.UtcNow;
    }

    public void ReduceStock(int qty)
    {
        if (QuantityAvailable < qty)
            throw new InvalidOperationException("Insufficient stock.");

        QuantityAvailable -= qty;
        LastUpdated = DateTime.UtcNow;
    }
}
