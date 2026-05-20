using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Tenant tenant, CancellationToken ct);
}