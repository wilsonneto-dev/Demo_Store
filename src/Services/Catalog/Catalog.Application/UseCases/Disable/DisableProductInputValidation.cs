using FluentValidation;

namespace Catalog.Application.UseCases.Disable;

public class DisableProductInputValidation : AbstractValidator<DisableProductInput>
{
    public DisableProductInputValidation() => RuleFor(x => x.Id).NotEmpty();
}