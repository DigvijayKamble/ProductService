namespace ProductService.Domain.Entities;

public class ProductPricing
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }

    public decimal Price { get; private set; }
    public string CurrencyCode { get; private set; }

    public DateTime EffectiveFrom { get; private set; }

    internal ProductPricing(Guid productId, decimal price, string currency)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Price = price;
        CurrencyCode = currency;
        EffectiveFrom = DateTime.UtcNow;
    }

    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }
}