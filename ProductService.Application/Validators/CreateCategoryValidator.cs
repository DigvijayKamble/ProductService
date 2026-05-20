using FluentValidation;
using ProductService.Application.DTOs;

namespace ProductService.Application.Validators; 

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}

