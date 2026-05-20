namespace ProductService.Domain.Entities;

public class MeasurementUnit
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Symbol { get; private set; } = null!;

    private MeasurementUnit() { }

    public MeasurementUnit(string name, string symbol)
    {
        Id = Guid.NewGuid();
        Name = name;
        Symbol = symbol;
    }
}

