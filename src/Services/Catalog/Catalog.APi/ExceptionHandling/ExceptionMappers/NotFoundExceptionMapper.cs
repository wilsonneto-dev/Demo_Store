using Microsoft.AspNetCore.Mvc;

namespace Catalog.APi.ExceptionHandling.ExceptionMappers;

public class NotFoundExceptionMapper : IExceptionMapper<Exception>
{
    public ProblemDetails Map(Exception exception, bool isDevelopment)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Not found",
            Status = StatusCodes.Status404NotFound,
            Type = StatusCodes.Status404NotFound.ToString(),
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