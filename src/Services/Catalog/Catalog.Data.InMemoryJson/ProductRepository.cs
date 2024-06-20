using System.Text.Json;
using Catalog.Domain.ProductAggregate;
using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Catalog.Data.InMemoryJson;

public class ProductRepository : IProductRepository
{
    private readonly Dictionary<string, string> _data = new ();
    
    public Task Insert(Product product, CancellationToken cancellationToken)
    {
        product.AddStock(123);
        var json = JsonSerializer.Serialize(product);
        _data.TryAdd(product.Id.ToString(), json);
        return Task.CompletedTask;
    }

    public Task Update(Product product, CancellationToken cancellationToken)
    {
        _data.Remove(product.Id.ToString());
        var json = JsonSerializer.Serialize(product);
        _data.TryAdd(product.Id.ToString(), json);
        return Task.CompletedTask;
    }

    public Task<Product?> Get(Guid id, CancellationToken cancellationToken)
    {
        if(!_data.TryGetValue(id.ToString(), out var json))
            return Task.FromResult<Product?>(null);

        // extract the data from persistence to a DTO
        var persistedObject = JsonDocument.Parse(json).RootElement;

        // usoing json converters
        var productJson = JsonSerializer.Deserialize<Product>(json); // anottations
        
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateSetterContractResolver()
        };
        
        var productNewton = JsonConvert.DeserializeObject<Product>(json, settings);
        
        // custom converter
        var options = new JsonSerializerOptions
        {
            Converters = { new ProductJsonConverter(new Dictionary<string, Type>
            {
                { "Name", typeof(string) },
                { "Price", typeof(decimal) },
                { "Description", typeof(string) },
                { "QuantityInStock", typeof(int) },
                { "Status", typeof(Status) },
                { "UpdatedDate", typeof(DateTime) }
            }) }
        };
        
        
        
        var deserializedProductCustom = JsonSerializer.Deserialize<Product>(json, options);
        
        return Task.FromResult<Product?>(null);
    }

    public Task<List<Product>> GetAll(CancellationToken cancellationToken) => 
        Task.FromResult<List<Product>>(Enumerable.Empty<Product>().ToList());
}