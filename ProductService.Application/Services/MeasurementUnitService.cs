using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Services;

public class MeasurementUnitService(IMeasurementUnitRepository repository) : IMeasurementUnitService
{
    private readonly IMeasurementUnitRepository _repository = repository; 

    public async Task<Guid> CreateAsync(CreateMeasurementUnitDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new InvalidOperationException("Name is required.");

        if (string.IsNullOrWhiteSpace(dto.Symbol))
            throw new InvalidOperationException("Symbol is required.");

        var unit = new MeasurementUnit(dto.Name, dto.Symbol);

        await _repository.AddAsync(unit);

        return unit.Id;
    }

    public async Task<IEnumerable<MeasurementUnitDto>> GetAllAsync()
    {
        var units = await _repository.GetAllAsync();

        return units.Select(u => new MeasurementUnitDto
        {
            Id = u.Id,
            Name = u.Name,
            Symbol = u.Symbol
        });
    }

    public async Task<MeasurementUnitDto?> GetByIdAsync(Guid id)
    {
        var unit = await _repository.GetByIdAsync(id);

        if (unit == null)
            return null;

        return new MeasurementUnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            Symbol = unit.Symbol
        };
    }
}
