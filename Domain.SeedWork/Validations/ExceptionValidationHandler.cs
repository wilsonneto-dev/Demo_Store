using Domain.SeedWork.Exceptions;

namespace Domain.SeedWork.Validations;

public class ExceptionValidationHandler : ValidationHandler
{
    public override void HandleError(ValidationError error) => throw new EntityValidationException(error.Message);
}