using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces; 

//public interface IProductRepository
//{
//    Task<Guid> AddAsync(Product product);
//    Task<Product?> GetByIdAsync(Guid id, bool includeUnpublished);
//    Task<Product?> UpdateAsync(Product product);
//    Task<IEnumerable<Product>> GetAllAsync();
//   // Task AddProductAttributeValueAsync(ProductAttributeValue attributeValue);
//   // Task AddProductAttributeAsync(ProductAttribute attribute);
//}

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Product product, CancellationToken cancellationToken);
    Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<Product?> DeleteAsync(Product product, CancellationToken cancellationToken);
}
