using Catalog.Application.Common.Exceptions;
using Catalog.Domain.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.UseCases.GetProduct;

public class GetProduct(
    IProductRepository productRepository,
    ILogger<GetProduct> logger) 
    : IRequestHandler<GetProductInput, GetProductOutput>
{
    public async Task<GetProductOutput> Handle(GetProductInput request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Consulting product {Id}", request.Id);
        var product = await productRepository.Get(request.Id, cancellationToken);
        NotFoundException.ThrowIfNull(product, $"Product '{request.Id}' not found.");
        return GetProductOutput.From(product!);
    }
}