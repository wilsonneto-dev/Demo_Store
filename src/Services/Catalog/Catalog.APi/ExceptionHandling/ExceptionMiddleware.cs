using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.APi.ExceptionHandling;

public class ExceptionMiddleware(
    RequestDelegate next, 
    ILogger<ExceptionMiddleware> logger, 
    IHostEnvironment env,
    IExceptionMapperResolver exceptionMapperResolver)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);            
        }
        catch (Exception ex)
        {
            await HandleException(ex, context);
        }
    }

    private async Task HandleException(Exception exception, HttpContext context)
    {
        var mapper = exceptionMapperResolver.Resolve(exception);
        var details = mapper.Map(exception, env.IsDevelopment());
        
        if(details.Status >= 500)
            logger.LogError("Exception: {Message}. {@Details}", details.Detail, details);
        else 
            logger.LogInformation("Exception: {Message}. {@Details}", details.Detail, details);

        context.Response.StatusCode = (int) (details.Status ?? StatusCodes.Status500InternalServerError);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(details));
    }
}