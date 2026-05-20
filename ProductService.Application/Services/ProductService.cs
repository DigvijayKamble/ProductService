using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Services;

public class ProductServices : IProductService
{
    private readonly IProductRepository _repository;

    public ProductServices(IProductRepository repository)
    {
        _repository = repository;
    }

    // ======================================================
    // CREATE PRODUCT
    // ======================================================
    public async Task<Guid> CreateAsync(CreateProductDto dto)
    {
        var product = new Product(
            dto.SKU,
            dto.Name,
            dto.CategoryId,
            dto.BrandId,
            dto.MeasurementUnitId,
            dto.BasePrice,
            dto.UnitValue,
            dto.InitialStock
        );

        //foreach (var optionId in dto.AttributeOptionIds)
        //    product.AddAttribute(optionId);

        foreach (var media in dto.Media)
            product.AddMedia(
                media.MediaURL,
                media.MediaType,
                media.IsPrimary,
                media.DisplayOrder);

        await _repository.AddAsync(product);

        return product.Id;
    }


    // ======================================================
    // GET BY ID
    // ======================================================
    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id, true);
        return product is null ? null : MapToDto(product);
    }

    // ======================================================
    // GET ALL
    // ======================================================
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(MapToDto);
    }

    // ======================================================
    // UPDATE
    // ======================================================
    public async Task<ProductDto> UpdateAsync(UpdateProductDto dto)
    {
        var product = await _repository.GetByIdAsync(dto.ProductId, true)
                      ?? throw new KeyNotFoundException("Product not found.");

        product.Update(
            dto.Name,
            dto.SKU,
            dto.CategoryId,
            dto.BrandId,
            dto.BasePrice,
            dto.CompareAtPrice
        );

        await _repository.UpdateAsync(product);

        return MapToDto(product);
    }

    // ======================================================
    // DELETE
    // ======================================================
    public async Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id, true)
                      ?? throw new KeyNotFoundException("Product not found.");

        product.SoftDelete();
        await _repository.UpdateAsync(product);
    }

    // ======================================================
    // PUBLISH
    // ======================================================
    public async Task PublishAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id, true)
                      ?? throw new KeyNotFoundException("Product not found.");

        product.Publish();
        await _repository.UpdateAsync(product);
    }

    // ======================================================
    // ADD ATTRIBUTE OPTION TO PRODUCT
    // ======================================================
    public async Task AddAttributeAsync(Guid productId, Guid attributeOptionId)
    {
        var product = await _repository.GetByIdAsync(productId, true)
                      ?? throw new KeyNotFoundException("Product not found.");

       // product.AddAttribute(attributeOptionId);

        await _repository.UpdateAsync(product);
    }

    // ======================================================
    // MAPPER
    // ======================================================
    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            ProductID = product.Id,
            SKU = product.SKU,
            ProductName = product.Name,
            CategoryID = product.CategoryId,
            BrandId = product.BrandId,
            BasePrice = product.BasePrice,
            CompareAtPrice = product.CompareAtPrice,
            IsPublished = product.IsPublished,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,

            //Attributes = product.Attributes.Select(a => new AttributeDto
            //{
            //    AttributeID = a.AttributeOption.Attribute.Id,
            //    AttributeName = a.AttributeOption.Attribute.Name,
            //    Value = a.AttributeOption.Value
            //}).ToList(),

            Media = product.Media.Select(m => new MediaDto
            {
                MediaURL = m.Url,
                MediaType = m.MediaType,
                IsPrimary = m.IsPrimary,
                DisplayOrder = m.DisplayOrder
            }).ToList()
        };
    }
}
