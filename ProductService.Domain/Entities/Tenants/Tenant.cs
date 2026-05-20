using ProductService.Domain.Common;

namespace ProductService.Domain.Entities;

public class Tenant : AggregateRoot
{
    public string Name { get; private set; }
    public bool IsActive { get; private set; }

    public Tenant(string name)
        : base(Guid.Empty) // Tenant does not belong to another tenant
    {
        Name = name;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}