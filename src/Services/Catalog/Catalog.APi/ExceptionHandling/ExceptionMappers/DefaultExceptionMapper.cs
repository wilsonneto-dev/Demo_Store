using Microsoft.AspNetCore.Mvc;

namespace Catalog.APi.ExceptionHandling.ExceptionMappers;

public class DefaultExceptionMapper : IExceptionMapper
{
    public Type TargetType => typeof(Exception);

    public ProblemDetails Map(Exception exception, bool isDevelopment)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An unexpected error happened",
            Status = StatusCodes.Status500InternalServerError,
            Type = exception.GetType().FullName!.Split(".").Last(),
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