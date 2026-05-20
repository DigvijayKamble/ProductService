using ProductService.Application.DTOs;

namespace ProductService.Application.Services;

public interface IMeasurementUnitService
{
    Task<Guid> CreateAsync(CreateMeasurementUnitDto dto);
    Task<IEnumerable<MeasurementUnitDto>> GetAllAsync();
    Task<MeasurementUnitDto?> GetByIdAsync(Guid id);
}
