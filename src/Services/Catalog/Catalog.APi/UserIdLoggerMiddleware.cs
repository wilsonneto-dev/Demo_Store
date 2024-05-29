namespace Catalog.APi;

public class UserIdLoggerMiddleware(RequestDelegate next, ILogger<UserIdLoggerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("user-id", out var userId))
        {
            using (logger.BeginScope(new Dictionary<string, object> { { "UserId", userId.ToString() } }))
            {
                await next(context);
            }
        }
        else
        {
            await next(context);
        }
    }
}