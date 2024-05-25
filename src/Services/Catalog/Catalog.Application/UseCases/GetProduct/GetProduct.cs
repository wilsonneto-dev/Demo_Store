using Catalog.Application.Common.Exceptions;
using Catalog.Domain.ProductAggregate;
using MediatR;

namespace Catalog.Application.UseCases.GetProduct;

public class GetProduct(
    IProductRepository productRepository) 
    : IRequestHandler<GetProductInput, GetProductOutput>
{
    public async Task<GetProductOutput> Handle(GetProductInput request, CancellationToken cancellationToken)
    {
        var product = await productRepository.Get(request.Id);
        NotFoundException.ThrowIfNull(product, $"Product '{request.Id}' not found.");
        return GetProductOutput.From(product);
    }
}