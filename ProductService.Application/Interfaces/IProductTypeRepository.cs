using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface IProductTypeRepository
{
    Task<ProductType?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(ProductType productType, CancellationToken ct);
}