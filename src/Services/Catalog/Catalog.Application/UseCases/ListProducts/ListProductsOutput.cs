using Catalog.Domain.ProductAggregate;

namespace Catalog.Application.UseCases.ListProducts;

public record ListProductsOutput(IReadOnlyList<ListProductsOutputItem> Products)
{
    public static ListProductsOutput From(IReadOnlyCollection<Product> products) =>
        new(products.Select(ListProductsOutputItem.From).ToList());
}