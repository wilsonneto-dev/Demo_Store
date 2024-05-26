namespace Catalog.APi.ExceptionHandling;

public interface IExceptionMapperResolver
{
    IExceptionMapper<Exception> Resolve(Exception exception);
}