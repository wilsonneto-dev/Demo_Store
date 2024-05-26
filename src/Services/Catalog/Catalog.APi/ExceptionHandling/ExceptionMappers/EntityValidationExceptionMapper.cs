using Domain.SeedWork.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.APi.ExceptionHandling.ExceptionMappers;

public class EntityValidationExceptionMapper : IExceptionMapper<Exception>
{
    public ProblemDetails Map(Exception exception, bool isDevelopment)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "One or more validation errors occurred",
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = StatusCodes.Status422UnprocessableEntity.ToString(),
            Detail = exception.Message,
        };
        
        if (isDevelopment)
        {
            problemDetails.Extensions.Add("StackTrace", exception.StackTrace);
            if(exception.InnerException is not null)
                problemDetails.Extensions.Add("InnerException", exception.InnerException);
            
        }
        
        return problemDetails;
    }
}