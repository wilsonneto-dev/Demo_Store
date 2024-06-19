using Catalog.Domain.ProductAggregate;

namespace Catalog.Application.UseCases.MoveStock;

public record MoveStockOutput(Guid Id, int QuantityInStock)
{
    public static MoveStockOutput From(Product product) =>
        new(product.Id, product.QuantityInStock);
}