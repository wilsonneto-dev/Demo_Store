using Catalog.Domain.ProductAggregate;

namespace Catalog.Application.UseCases.Enable;

public record EnableProductOutput(Guid Id, Status Status)
{
    public static EnableProductOutput From(Product product) =>
        new(product.Id, product.Status);
}