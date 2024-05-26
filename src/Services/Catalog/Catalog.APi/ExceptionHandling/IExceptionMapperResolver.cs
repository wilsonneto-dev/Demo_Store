namespace Catalog.APi.ExceptionHandling;

public interface IExceptionMapperResolver
{
    IExceptionMapper Resolve(Exception exception);
}