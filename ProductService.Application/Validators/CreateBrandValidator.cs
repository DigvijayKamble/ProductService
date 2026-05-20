using FluentValidation; 
using ProductService.Application.DTOs;

namespace ProductService.Application.Validators; 

public class CreateBrandValidator : AbstractValidator<CreateBrandDto>
{
    public CreateBrandValidator()
    {
        RuleFor(x => x.BrandName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);
    }
}
