using Catalog.APi.ExceptionHandling.ExceptionMappers;
using Domain.SeedWork.Exceptions;

namespace Catalog.APi.ExceptionHandling;

public class ExceptionMapperResolver(
    IServiceProvider serviceProvider,
    Dictionary<Type, Type> exceptionHandlers)
    : IExceptionMapperResolver
{
    public IExceptionMapper<Exception> Resolve(Exception exception)
    {
        var exceptionType = exception.GetType();
        if (exceptionHandlers.TryGetValue(exceptionType, out var handlerType))
            return (IExceptionMapper<Exception>) serviceProvider.GetService(handlerType)!;
        return new DefaultExceptionMapper();
    }
}