namespace ProductService.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string SKU { get; private set; } = null!;
    public string Name { get; private set; } = null!; 
    public Guid CategoryId { get; private set; }
    public Guid BrandId { get; private set; }
    public Guid MeasurementUnitId { get; private set; } 
    public decimal BasePrice { get; private set; }
    public decimal? CompareAtPrice { get; private set; }
    public decimal? UnitValue { get; private set; } 
    public bool IsPublished { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; } 
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; } 
    public Inventory Inventory { get; private set; } = null!;

    //private readonly List<ProductAttributeValue> _attributes = new();
    //public IReadOnlyCollection<ProductAttribute> Attributes => _attributes;

    private readonly List<ProductMedia> _media = new();
    public IReadOnlyCollection<ProductMedia> Media => _media;

    private Product() { } // EF

    // =============================================
    // CONSTRUCTOR
    // =============================================
    public Product(
        string sku,
        string name,
        Guid categoryId,
        Guid brandId,
        Guid measurementUnitId,
        decimal basePrice,
        decimal? unitValue,
        int initialStock)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new InvalidOperationException("SKU is required.");

        if (basePrice <= 0)
            throw new InvalidOperationException("Base price must be greater than zero.");

        Id = Guid.NewGuid();
        SKU = sku;
        Name = name;
        CategoryId = categoryId;
        BrandId = brandId;
        MeasurementUnitId = measurementUnitId;
        BasePrice = basePrice;
        UnitValue = unitValue;

        IsActive = true;
        IsPublished = false;
        IsDeleted = false;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Inventory = new Inventory(Id, initialStock);
    }

    // =============================================
    // UPDATE PRODUCT DETAILS
    // =============================================
    public void Update(
        string name,
        string sku,
        Guid categoryId,
        Guid brandId,
        decimal basePrice,
        decimal? compareAtPrice)
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot update deleted product.");

        if (string.IsNullOrWhiteSpace(sku))
            throw new InvalidOperationException("SKU cannot be empty.");

        if (basePrice <= 0)
            throw new InvalidOperationException("Price must be greater than zero.");

        Name = name;
        SKU = sku;
        CategoryId = categoryId;
        BrandId = brandId;
        BasePrice = basePrice;
        CompareAtPrice = compareAtPrice;

        UpdatedAt = DateTime.UtcNow;
    }

    // =============================================
    // SOFT DELETE
    // =============================================
    public void SoftDelete()
    {
        if (IsDeleted)
            return;

        IsDeleted = true;
        IsActive = false;
        IsPublished = false;
        UpdatedAt = DateTime.UtcNow;
    }

    // =============================================
    // PUBLISH
    // =============================================
    public void Publish()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Deleted product cannot be published.");

        if (!IsActive)
            throw new InvalidOperationException("Inactive product cannot be published.");

        IsPublished = true;
        UpdatedAt = DateTime.UtcNow;
    }

    // =============================================
    // DEACTIVATE
    // =============================================
    public void Deactivate()
    {
        IsActive = false;
        IsPublished = false;
        UpdatedAt = DateTime.UtcNow;
    }

    // =============================================
    // PRICE UPDATE
    // =============================================
    public void ChangePrice(decimal newPrice, decimal? compareAtPrice = null)
    {
        if (newPrice <= 0)
            throw new InvalidOperationException("Price must be greater than zero.");

        BasePrice = newPrice;
        CompareAtPrice = compareAtPrice;
        UpdatedAt = DateTime.UtcNow;
    }

    // =============================================
    // ATTRIBUTE MANAGEMENT
    // =============================================
    //public void AddAttribute(Guid attributeId, string value)
    //{
    //    if (_attributes.Any(a => a.AttributeOptionId == attributeId))
    //        throw new InvalidOperationException("Attribute already exists.");

    //    _attributes.Add(new ProductAttributeValue(attributeId, value));
    //    UpdatedAt = DateTime.UtcNow;
    //}
    //public void AddAttribute(Guid optionId)
    //{
    //    if (_attributes.Any(a => a.AttributeOptionId == optionId))
    //        throw new InvalidOperationException("Attribute already exists.");

    //    _attributes.Add(new ProductAttributeValue(Id, optionId));
    //    UpdatedAt = DateTime.UtcNow;
    //}


    //public void RemoveAttribute(Guid attributeId)
    //{
    //    var attr = _attributes.FirstOrDefault(a => a.AttributeOptionId == attributeId);
    //    if (attr != null)
    //        _attributes.Remove(attr);

    //    UpdatedAt = DateTime.UtcNow;
    //}

    // =============================================
    // MEDIA MANAGEMENT
    // =============================================
    public void AddMedia(string url, string type, bool isPrimary, int order)
    {
        if (isPrimary)
        {
            foreach (var media in _media)
                media.RemovePrimary();
        }

        _media.Add(new ProductMedia(Id, url, type, isPrimary, order));
        UpdatedAt = DateTime.UtcNow;
    }
}

public class ProductAttribute
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string DataType { get; private set; } = null!;

    private readonly List<AttributeOption> _options = new();
    public IReadOnlyCollection<AttributeOption> Options => _options;

    private ProductAttribute() { }

    public ProductAttribute(string name, string dataType)
    {
        Id = Guid.NewGuid();
        Name = name;
        DataType = dataType;
    }

    public void AddOption(string value)
    {
        if (_options.Any(o => o.Value == value))
            throw new InvalidOperationException("Option already exists.");

        _options.Add(new AttributeOption(Id, value));
    }
}
public class AttributeOption
{
    public Guid Id { get; private set; }
    public Guid AttributeId { get; private set; }
    public string Value { get; private set; } = null!;

    // 🔥 ADD THIS
    public ProductAttribute Attribute { get; private set; } = null!;

    private AttributeOption() { }

    public AttributeOption(Guid attributeId, string value)
    {
        Id = Guid.NewGuid();
        AttributeId = attributeId;
        Value = value;
    }
}

//public class AttributeOption
//{
//    public Guid Id { get; private set; }
//    public Guid AttributeId { get; private set; }
//    public string Value { get; private set; } = null!;

//    private AttributeOption() { }

//    public AttributeOption(Guid attributeId, string value)
//    {
//        Id = Guid.NewGuid();
//        AttributeId = attributeId;
//        Value = value;
//    }
//    public ProductAttribute Attribute { get; private set; } = null!;
//}
public class ProductAttributeValue
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid AttributeOptionId { get; private set; }

    private ProductAttributeValue() { }

    public ProductAttributeValue(Guid productId, Guid optionId)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        AttributeOptionId = optionId;
    }
}


public class ProductMedia
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Url { get; private set; } = null!;
    public string MediaType { get; private set; } = null!;
    public bool IsPrimary { get; private set; }
    public int DisplayOrder { get; private set; }

    private ProductMedia() { }

    public ProductMedia(Guid productId, string url, string type, bool isPrimary, int order)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Url = url;
        MediaType = type;
        IsPrimary = isPrimary;
        DisplayOrder = order;
    }

    public void RemovePrimary()
    {
        IsPrimary = false;
    }
}
