namespace ProductService.Application.DTOs;

public record CreateProductCommand(
    Guid TenantId,
    string Name,
    Guid ProductTypeId
);