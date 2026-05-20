namespace ProductService.Application.DTOs;

public class CreateMeasurementUnitDto
{
    public string Name { get; set; } = null!;
    public string Symbol { get; set; } = null!;
}

public class MeasurementUnitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Symbol { get; set; } = null!;
}

