using Catalog.Application.Common.Contracts;
using Catalog.Domain.ProductAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Data.InMemoryJson;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        return services;
    }
}