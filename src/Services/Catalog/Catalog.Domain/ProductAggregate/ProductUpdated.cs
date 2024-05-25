using Domain.SeedWork;

namespace Catalog.Domain.ProductAggregate;

public sealed class ProductUpdated(Guid productId, string name, decimal price) 
    : DomainEvent
{
    public Guid ProductId { get; private set; } = productId;
    public string Name { get; private set; } = name;
    public decimal Price { get; private set; } = price;
}