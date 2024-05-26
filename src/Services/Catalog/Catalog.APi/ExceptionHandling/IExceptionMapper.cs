using Microsoft.AspNetCore.Mvc;

namespace Catalog.APi.ExceptionHandling;

public interface IExceptionMapper 
{
    Type TargetType { get; }
    
    ProblemDetails Map(Exception exception, bool isDevelopment);
}