using FluentValidation;

namespace Catalog.Application.UseCases.GetProduct;

public class GetProductInputValidation : AbstractValidator<GetProductInput>
{
    public GetProductInputValidation() => 
        RuleFor(x => x.Id).NotEmpty();
}