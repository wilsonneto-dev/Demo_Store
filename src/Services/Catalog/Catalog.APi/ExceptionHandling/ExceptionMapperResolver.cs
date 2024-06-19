using Catalog.APi.ExceptionHandling.ExceptionMappers;

namespace Catalog.APi.ExceptionHandling;

public class ExceptionMapperResolver : IExceptionMapperResolver
{
    private readonly Dictionary<Type, IExceptionMapper> _exceptionHandlers;
    private readonly IExceptionMapper _defaultMapper = new DefaultExceptionMapper();

    public ExceptionMapperResolver(IEnumerable<IExceptionMapper> exceptionMappers)
    {
        _exceptionHandlers = new Dictionary<Type, IExceptionMapper>();
        foreach (var mapper in exceptionMappers)
            _exceptionHandlers.TryAdd(mapper.TargetType, mapper);
    }

    public IExceptionMapper Resolve(Exception exception)
    {
        _exceptionHandlers.TryGetValue(exception.GetType(), out var mapper);
        return mapper ?? _defaultMapper;
    }
}