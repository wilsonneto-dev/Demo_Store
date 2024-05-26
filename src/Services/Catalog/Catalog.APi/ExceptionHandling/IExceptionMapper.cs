using Microsoft.AspNetCore.Mvc;

namespace Catalog.APi.ExceptionHandling;

public interface IExceptionMapper<in T> where T : Exception
{
    ProblemDetails Map(T exception, bool isDevelopment);
}