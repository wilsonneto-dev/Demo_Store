using FluentValidation;

namespace Catalog.Application.UseCases.CreateProduct;

public class CreateProductInputValidation : AbstractValidator<CreateProductInput>
{
    public CreateProductInputValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}