using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces;

public interface IMeasurementUnitRepository
{
    Task AddAsync(MeasurementUnit unit);
    Task<IEnumerable<MeasurementUnit>> GetAllAsync();
    Task<MeasurementUnit?> GetByIdAsync(Guid id);
}

