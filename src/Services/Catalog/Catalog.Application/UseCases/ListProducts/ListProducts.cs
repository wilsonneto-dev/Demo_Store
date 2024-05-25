using Catalog.Domain.ProductAggregate;
using MediatR;

namespace Catalog.Application.UseCases.ListProducts;

public class ListProducts(IProductRepository productRepository) 
    : IRequestHandler<ListProductsInput, ListProductsOutput>
{
    public async Task<ListProductsOutput> Handle(ListProductsInput _, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAll();
        return ListProductsOutput.From(products);
    }
}