using ProductService.Domain.Common;

namespace ProductService.Domain.Entities;

public class Brand //: AuditableEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;

    private Brand() { }

    public Brand(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
       // MarkUpdated();
    }
}
