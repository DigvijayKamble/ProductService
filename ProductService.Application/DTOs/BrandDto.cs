namespace ProductService.Application.DTOs; 
public class CreateBrandDto
{
    public string BrandName { get; set; } = null!;
}
 
public class BrandDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class UpdateBrandDto
{
    public string Name { get; set; } = null!;
}