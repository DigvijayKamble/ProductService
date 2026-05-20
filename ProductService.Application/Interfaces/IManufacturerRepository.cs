using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface IManufacturerRepository
{
    Task<Manufacturer?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Manufacturer manufacturer, CancellationToken ct);
}
