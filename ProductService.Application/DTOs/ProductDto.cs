using ProductService.Domain.Entities;

namespace ProductService.Application.DTOs;

public class CreateProductDto
{
    public string SKU { get; set; } = null!;
    public string Name { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public Guid MeasurementUnitId { get; set; }

    public decimal BasePrice { get; set; }
    public decimal? UnitValue { get; set; }
    public decimal? CompareAtPrice { get; set; }

    public int InitialStock { get; set; }

    public List<Guid> AttributeOptionIds { get; set; } = new();

    public List<CreateMediaDto> Media { get; set; } = new();
}

public class UpdateProductDto
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;
    public string SKU { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }

    public decimal BasePrice { get; set; }
    public decimal? CompareAtPrice { get; set; }
}


/// <summary>
/// Represents a data transfer object (DTO) that contains detailed information about a product, including identifiers,
/// descriptive fields, pricing, publication status, and related attributes and media.
/// </summary>
/// <remarks>This class is typically used to transfer product data between application layers, such as between an
/// API and a client. It includes both core product information and related metadata, such as category and brand
/// details, as well as collections of product attributes and media items. All properties are intended for data
/// transport and may be used for display, editing, or storage purposes.</remarks>
public class ProductDto
{
    public Guid ProductID { get; set; }
    public string SKU { get; set; } = null!;
    public string ProductName { get; set; } = null!;

    public Guid CategoryID { get; set; }
    public string? CategoryName { get; set; }

    public Guid BrandId { get; set; }
    public string? BrandName { get; set; }

    public string ShortDescription { get; set; } = null!;
    public string LongDescription { get; set; } = null!;

    public decimal BasePrice { get; set; }
    public decimal? CompareAtPrice { get; set; }

    public bool IsPublished { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } = string.Empty!;
    public string UpdatedBy { get; set; } = string.Empty!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<AttributeDto> Attributes { get; set; }
        = new();

    public List<MediaDto> Media { get; set; }
        = new();
} 
/// <summary>
/// Represents the data required to create a new product attribute.
/// </summary>
public class CreateAttributeDto
{
    public Guid AttributeOptionId { get; set; }
    public Guid AttributeID { get; set; }
    public string AttributeName { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public string Value { get; set; } = null!;
}
/// <summary>
/// 
/// </summary>
public class AttributeDto
{
    public Guid AttributeID { get; set; }
    public string AttributeName { get; set; } = null!;
    public string Value { get; set; } = null!; 
}

public class ProductAttributeValue
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid AttributeOptionId { get; private set; }

    // 🔥 THIS IS MISSING IN YOUR CODE
    public AttributeOption AttributeOption { get; private set; } = null!;

    private ProductAttributeValue() { }

    public ProductAttributeValue(Guid productId, Guid optionId)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        AttributeOptionId = optionId;
    }
}

public class AttributeOption
{
    public Guid Id { get; private set; }
    public Guid AttributeId { get; private set; }
    public string Value { get; private set; } = null!;

    // 🔥 REQUIRED
    public ProductAttribute Attribute { get; private set; } = null!;

    private AttributeOption() { }

    public AttributeOption(Guid attributeId, string value)
    {
        Id = Guid.NewGuid();
        AttributeId = attributeId;
        Value = value;
    }
}


/// <summary>
/// Represents the data required to create a new media item associated with a product.
/// </summary>
/// <remarks>Use this data transfer object when adding images, videos, or other media types to a product. The
/// properties specify the media's URL, type, display order, and whether it is the primary media for the
/// product.</remarks>
public class CreateMediaDto
{
    public string MediaURL { get; set; } = null!;
    public string MediaType { get; set; } = null!;
    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }
}
/// <summary>
/// Represents media information associated with a product, such as images or videos, for use in product listings or
/// galleries.
/// </summary>
public class MediaDto
{
    public string MediaURL { get; set; } = null!;
    public string MediaType { get; set; } = null!;
    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }
}


