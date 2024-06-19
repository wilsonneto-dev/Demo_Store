using Catalog.Domain.ProductAggregate;

namespace Catalog.Application.UseCases.GetProduct;

public record GetProductOutput(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int QuantityInStock)
{
    public static GetProductOutput From(Product product) =>
        new(product.Id, product.Name, product.Description, product.Price, product.QuantityInStock);
}
