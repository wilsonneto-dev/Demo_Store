using Catalog.APi.ExceptionHandling;
using Catalog.APi.ExceptionHandling.ExceptionMappers;
using Catalog.Application;
using Catalog.Application.Common.Exceptions;
using Catalog.Application.UseCases.CreateProduct;
using Catalog.Application.UseCases.GetProduct;
using Catalog.Application.UseCases.ListProducts;
using Catalog.Infra.Data.EF;
using Domain.SeedWork.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfraData();

builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
    options.LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging();
});

builder.Services.AddTransient<IExceptionMapper, EntityValidationExceptionMapper>();
builder.Services.AddTransient<IExceptionMapper, NotFoundExceptionMapper>();
builder.Services.AddSingleton<IExceptionMapperResolver, ExceptionMapperResolver>();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(cfg => cfg.EndpointPathPrefix = "docs");
app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
app.UseMiddleware<ExceptionMiddleware>();

var productsRouter = app.MapGroup("/products");

productsRouter.MapPost("", async (CreateProductInput input, IMediator mediator, CancellationToken cancellationToken) => 
    await mediator.Send(input, cancellationToken));

productsRouter.MapGet("", async (IMediator mediator, CancellationToken cancellationToken) =>
    await mediator.Send(new ListProductsInput(), cancellationToken));

productsRouter.MapGet("{id:guid}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
    await mediator.Send(new GetProductInput(id), cancellationToken));

app.Run();
