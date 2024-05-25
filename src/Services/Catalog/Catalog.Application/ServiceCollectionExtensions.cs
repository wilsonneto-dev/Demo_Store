using Application.Shared.Behaviors;
using Catalog.Application.UseCases.CreateProduct;
using Catalog.Application.UseCases.GetProduct;
using Catalog.Application.UseCases.ListProducts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddMediatrAndDefaultBehaviors(services);
        AddUseCases(services);
        return services;
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CreateProductInput, CreateProductOutput>, CreateProduct>();
        services.AddTransient<AbstractValidator<CreateProductInput>, CreateProductInputValidation>();
        services.AddTransient<IRequestHandler<GetProductInput, GetProductOutput>, GetProduct>();
        services.AddTransient<AbstractValidator<GetProductInput>, GetProductInputValidation>();
        services.AddTransient<IRequestHandler<ListProductsInput, ListProductsOutput>, ListProducts>();
    }

    private static void AddMediatrAndDefaultBehaviors(IServiceCollection services)
    {
        services.AddMediatR(c => {});
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}