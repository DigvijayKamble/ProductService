namespace ProductService.Domain.Common;

public interface IAggregateRoot { }

public abstract class AggregateRoot : BaseEntity, IAggregateRoot
{
    protected AggregateRoot(Guid tenantId) : base(tenantId)
    {
    }
}