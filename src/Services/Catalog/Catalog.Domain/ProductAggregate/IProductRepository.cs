namespace Catalog.Domain.ProductAggregate;

public interface IProductRepository
{
    Task Insert(Product product);
    Task<Product> Get(Guid id);
    Task<IReadOnlyCollection<Product>> GetAll();
}