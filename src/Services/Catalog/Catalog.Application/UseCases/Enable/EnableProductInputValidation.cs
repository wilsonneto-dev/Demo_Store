using FluentValidation;

namespace Catalog.Application.UseCases.Enable;

public class EnableProductInputValidation : AbstractValidator<EnableProductInput>
{
    public EnableProductInputValidation() =>
        RuleFor(x => x.Id).NotEmpty();
}