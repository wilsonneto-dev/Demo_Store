using Catalog.Application.Common.Contracts;
using Catalog.Application.Common.Exceptions;
using Catalog.Domain.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.UseCases.MoveStock;

public class MoveStock(
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    ILogger<MoveStock> logger)
    : IRequestHandler<MoveStockInput, MoveStockOutput>
{
    public async Task<MoveStockOutput> Handle(
        MoveStockInput request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Moving stock for product. {@Request}", request);

        var product = await productRepository.Get(request.Id, cancellationToken);
        NotFoundException.ThrowIfNull(product, $"Product '{request.Id}' not found.");

        if (request.MovementType == MovementType.Add)
            product!.AddStock(request.Quantity);
        else 
            product!.RemoveStock(request.Quantity);

        await productRepository.Update(product, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        logger.LogInformation("Stock for product {Id} moved. {@Request}", product.Id, request);
        return MoveStockOutput.From(product);
    }
}