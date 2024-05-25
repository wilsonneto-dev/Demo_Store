namespace Catalog.Domain.ProductAggregate;

public interface IProductRepository
{
    Task Insert(Product product, CancellationToken cancellationToken);
    Task<Product?> Get(Guid id, CancellationToken cancellationToken);
    Task<List<Product>> GetAll(CancellationToken cancellationToken);
}