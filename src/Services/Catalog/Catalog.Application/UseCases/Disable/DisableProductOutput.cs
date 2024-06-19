using Catalog.Domain.ProductAggregate;

namespace Catalog.Application.UseCases.Disable;

public record DisableProductOutput(Guid Id, Status Status)
{
    public static DisableProductOutput From(Product product) =>
        new(product.Id, product.Status);
}