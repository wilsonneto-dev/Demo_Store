using FluentValidation;

namespace Catalog.Application.UseCases.MoveStock;

public class MoveStockInputValidation : AbstractValidator<MoveStockInput>
{
    public MoveStockInputValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.MovementType).IsInEnum();
    }
}