using FluentValidation; 
using ProductService.Application.DTOs;

namespace ProductService.Application.Validators; 

public class CreateProductValidator : AbstractValidator<ProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty();
        RuleFor(x => x.SKU).NotEmpty();
        RuleFor(x => x.BasePrice).GreaterThan(0);
    }
}

