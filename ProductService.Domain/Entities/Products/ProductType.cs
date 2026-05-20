using ProductService.Domain.Common;

namespace ProductService.Domain.Entities;

public class ProductType : AggregateRoot
{
    public string Name { get; private set; }

    public ProductType(Guid tenantId, string name)
        : base(tenantId)
    {
        Name = name;
    }

    public void Rename(string name)
    {
        Name = name;
        ModifiedOnUtc = DateTime.UtcNow;
    }
}