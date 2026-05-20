using ProductService.Domain.Common;

namespace ProductService.Domain.Entities;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public Guid? ParentCategoryId { get; private set; }

    public Category(Guid tenantId, string name)
        : base(tenantId)
    {
        Name = name;
    }
}
