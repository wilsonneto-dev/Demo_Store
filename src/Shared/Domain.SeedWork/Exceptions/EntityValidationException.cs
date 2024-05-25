using Domain.SeedWork.Validations;

namespace Domain.SeedWork.Exceptions;

public class EntityValidationException(
    string? message,
    IReadOnlyCollection<ValidationError>? errors = null)
    : DomainException(message)
{
    public IReadOnlyCollection<ValidationError>? Errors { get; } = errors;
}