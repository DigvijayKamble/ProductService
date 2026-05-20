using ProductService.Domain.Common;

namespace ProductService.Domain.Entities;

public class Product : AggregateRoot
{
    private readonly List<ProductPricing> _pricings = new();
    private readonly List<ProductInventory> _inventories = new();

    public string Name { get; private set; }
    public string? ShortDescription { get; private set; }
    public string? FullDescription { get; private set; }
    public bool IsPublished { get; private set; }

    private ProductSEO? _seo;
    public ProductSEO? SEO => _seo;
    public Guid ProductTypeId { get; private set; }

    public IReadOnlyCollection<ProductPricing> Pricings => _pricings;
    public IReadOnlyCollection<ProductInventory> Inventories => _inventories;

    public Product(
        Guid tenantId,
        string name,
        Guid productTypeId) : base(tenantId)
    {
        Name = name;
        ProductTypeId = productTypeId;
        IsPublished = false;
    }

    public void UpdateDetails(string name, string? shortDesc, string? fullDesc)
    {
        Name = name;
        ShortDescription = shortDesc;
        FullDescription = fullDesc;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public void Publish()
    {
        if (!_pricings.Any())
            throw new InvalidOperationException("Product must have pricing before publishing.");

        IsPublished = true;
    }

    public void AddPricing(decimal price, string currency)
    {
        var pricing = new ProductPricing(Id, price, currency);
        _pricings.Add(pricing);
    }

    public void AddInventory(Guid warehouseId, int quantity)
    {
        var inventory = new ProductInventory(Id, warehouseId, quantity);
        _inventories.Add(inventory);
    }

    public void SetSEO(string slug, string? metaTitle, string? metaDescription)
    {
        _seo = new ProductSEO(Id, slug, metaTitle, metaDescription);
    }
}