using Catalog.Domain.ProductAggregate;

namespace Catalog.Data.InMemoryJson;

public class ProductRepository : IProductRepository
{
    private readonly Dictionary<string, Product> _data = new ();
    
    public Task Insert(Product product, CancellationToken cancellationToken)
    {
        _data.TryAdd(product.Id.ToString(), product);
        return Task.CompletedTask;
    }

    public Task Update(Product product, CancellationToken cancellationToken)
    {
        _data.Remove(product.Id.ToString());
        _data.TryAdd(product.Id.ToString(), product);
        return Task.CompletedTask;
    }

    public Task<Product?> Get(Guid id, CancellationToken cancellationToken) => 
        Task.FromResult(_data.ContainsKey(id.ToString()) ? _data[id.ToString()] : null);

    public Task<List<Product>> GetAll(CancellationToken cancellationToken) =>
        Task.FromResult(_data.Values.ToList());
}