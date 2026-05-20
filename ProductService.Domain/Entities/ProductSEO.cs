namespace ProductService.Domain.Entities;

public class ProductSEO
{
    public int ProductId { get; set; }
    public string Slug { get; set; } = null!;
    public string? MetaTitle { get; set; } 
    public Product Product { get; set; } = null!;
}
