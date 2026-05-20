namespace ProductService.Application.DTOs;

public class CreateCategoryDto
{
    public string Name { get; set; } = null!;
}

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class UpdateCategoryDto
{
    public string Name { get; set; } = null!;
}
