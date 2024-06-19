using Catalog.Application.Common.Contracts;
using Catalog.Application.Common.Exceptions;
using Catalog.Domain.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.UseCases.Disable;

public class DisableProduct(
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    ILogger<DisableProduct> logger)
    : IRequestHandler<DisableProductInput, DisableProductOutput>
{
    public async Task<DisableProductOutput> Handle(
        DisableProductInput request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Disabling product. {@Request}", request);

        var product = await productRepository.Get(request.Id, cancellationToken);
        NotFoundException.ThrowIfNull(product, $"Product '{request.Id}' not found.");
        
        product!.Deactivate();

        await productRepository.Update(product, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        logger.LogInformation("Product {Id} disabled. {@Product}", product.Id, product);
        return DisableProductOutput.From(product);
    }
}