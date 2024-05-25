using Catalog.Domain.ProductAggregate;

namespace Catalog.Application.UseCases.CreateProduct;

public record CreateProductOutput(Guid Id)
{
    public static CreateProductOutput From(Product product) =>
        new(product.Id);
}
