using Domain.SeedWork.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.APi.ExceptionHandling.ExceptionMappers;

public class EntityValidationExceptionMapper : IExceptionMapper
{
    public Type TargetType => typeof(EntityValidationException);

    public ProblemDetails Map(Exception exception, bool isDevelopment)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "One or more validation errors occurred",
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = StatusCodes.Status422UnprocessableEntity.ToString(),
            Detail = exception.Message,
        };

        if (exception is EntityValidationException validationException)
        {
            problemDetails.Detail = string.Join(';', validationException.Errors!.Select(x => x.Message));
            problemDetails.Extensions.Add("validations", validationException.Errors);
        }
        
        if (isDevelopment)
        {
            problemDetails.Extensions.Add("stackTrace", exception.StackTrace);
            if(exception.InnerException is not null)
                problemDetails.Extensions.Add("innerException", exception.InnerException);
            
        }
        
        return problemDetails;
    }
}