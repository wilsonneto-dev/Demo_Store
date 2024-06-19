using Catalog.Application.Common.Contracts;
using Catalog.Application.Common.Exceptions;
using Catalog.Domain.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.UseCases.Enable;

public class EnableProduct(
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    ILogger<EnableProduct> logger)
    : IRequestHandler<EnableProductInput, EnableProductOutput>
{
    public async Task<EnableProductOutput> Handle(
        EnableProductInput request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Enabling product. {@Request}", request);

        var product = await productRepository.Get(request.Id, cancellationToken);
        NotFoundException.ThrowIfNull(product, $"Product '{request.Id}' not found.");

        product!.Activate();

        await productRepository.Update(product, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        logger.LogInformation("Product {Id} enabled. {@Product}", product.Id, product);
        return EnableProductOutput.From(product);
    }
}