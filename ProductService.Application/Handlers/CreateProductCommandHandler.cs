using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Handlers;

public class CreateProductCommandHandler
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateProductCommand command, CancellationToken ct)
    {
        var product = new Product(
            command.TenantId,
            command.Name,
            command.ProductTypeId);

        await _repository.AddAsync(product, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return product.Id;
    }
}