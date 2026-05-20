namespace ProductService.Domain.Entities;

public class ProductPricing
{
    public int PricingId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public string CurrencyCode { get; set; } = "INR";
    public Product Product { get; set; } = null!;
}