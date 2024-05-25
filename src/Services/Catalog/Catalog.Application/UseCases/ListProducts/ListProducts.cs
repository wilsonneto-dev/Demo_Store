using Catalog.Domain.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.UseCases.ListProducts;

public class ListProducts(
    IProductRepository productRepository, 
    ILogger<ListProducts> logger) 
    : IRequestHandler<ListProductsInput, ListProductsOutput>
{
    public async Task<ListProductsOutput> Handle(ListProductsInput _, CancellationToken cancellationToken)
    {
        logger.LogInformation("Listing products");
        var products = await productRepository.GetAll(cancellationToken);
        return ListProductsOutput.From(products);
    }
}