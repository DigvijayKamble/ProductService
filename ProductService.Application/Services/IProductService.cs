using ProductService.Application.DTOs;

namespace ProductService.Application.Services;
public interface IProductService
{
    Task<Guid> CreateAsync(CreateProductDto dto);
    Task<ProductDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> UpdateAsync(UpdateProductDto product); 
    Task PublishAsync(Guid productId);
    Task DeleteAsync(Guid productId);
    Task AddAttributeAsync(Guid productId, Guid attributeOptionId);
}

