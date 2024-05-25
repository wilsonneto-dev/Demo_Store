using Domain.SeedWork.Exceptions;

namespace Domain.SeedWork.Validations;

public class DomainValidation
{
    public static void NotNull(object? target, string fieldName, ValidationHandler? handler = null)
    {
        if (target is not null) return;
        var message = $"{fieldName} should not be null";
        if (handler is null) throw new EntityValidationException(message);
        handler.HandleError(message);
    }

    public static void NotNullOrEmpty(string? target, string fieldName, ValidationHandler? handler = null)
    {
        if (!string.IsNullOrWhiteSpace(target)) return;
        var message = $"{fieldName} should not be empty or null";
        if (handler is null) throw new EntityValidationException(message);
        handler.HandleError(message);
    }

    public static void MinLength(string target, int minLength, string fieldName, ValidationHandler? handler = null)
    {
        if (target.Length >= minLength) return;
        var message = $"{fieldName} should be at least {minLength} characters long";
        if(handler is null) throw new EntityValidationException(message);
        handler.HandleError(message);
    }

    public static void MaxLength(string target, int maxLength, string fieldName, ValidationHandler? handler = null)
    {
        if (target.Length <= maxLength) return;
        var message = $"{fieldName} should be less or equal {maxLength} characters long";
        if(handler is null) throw new EntityValidationException(message);
        handler.HandleError(message);
    }
}