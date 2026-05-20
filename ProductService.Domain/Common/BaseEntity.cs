namespace ProductService.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    public Guid TenantId { get; protected set; }

    public bool IsDeleted { get; protected set; }

    public DateTime CreatedOnUtc { get; protected set; }
    public string? CreatedBy { get; protected set; }

    public DateTime? ModifiedOnUtc { get; protected set; }
    public string? ModifiedBy { get; protected set; }

    protected BaseEntity(Guid tenantId)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}