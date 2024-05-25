using Catalog.Domain.ProductAggregate;

namespace Catalog.Application.UseCases.ListProducts;

public record ListProductsOutputItem(Guid Id, string Name)
{
    public static ListProductsOutputItem From(Product product) =>
        new(product.Id, product.Name);
}