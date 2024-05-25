namespace Domain.SeedWork.Validations;

public abstract class Validator
{
    protected readonly ValidationHandler _handler;

    protected Validator(ValidationHandler handler)
        => _handler = handler;

    public abstract void Validate();
}