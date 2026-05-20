namespace ProductService.Contracts.Events;

public record ProductCreatedEvent(
    Guid ProductId,
    string SKU,
    string ProductName
);

