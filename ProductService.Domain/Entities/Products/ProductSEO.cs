namespace ProductService.Domain.Entities;

public class ProductSEO
{
    public Guid ProductId { get; private set; }

    public string Slug { get; private set; }
    public string? MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? MetaKeywords { get; private set; }

    internal ProductSEO(
        Guid productId,
        string slug,
        string? metaTitle,
        string? metaDescription)
    {
        ProductId = productId;
        Slug = slug;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
    }

    public void Update(string slug, string? title, string? description)
    {
        Slug = slug;
        MetaTitle = title;
        MetaDescription = description;
    }
}
