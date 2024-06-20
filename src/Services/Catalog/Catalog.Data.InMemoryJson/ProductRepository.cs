using System.Text.Json;
using Catalog.Domain.ProductAggregate;
using Catalog.Domain.ProductAggregate.Loaders;
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
        var productNewton = JsonConvert.DeserializeObject<Product>(json);
        
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
        
        // load method
        var _product = Product.Load(
                persistedObject.GetProperty("Id").GetGuid(),
                persistedObject.GetProperty("Name").GetString()!,
                persistedObject.GetProperty("Description").GetString()!,
                persistedObject.GetProperty("Price").GetDecimal(),
                persistedObject.GetProperty("QuantityInStock").GetInt32(),
                (Status)persistedObject.GetProperty("Status").GetInt32(),
                persistedObject.GetProperty("CreatedDate").GetDateTimeOffset().DateTime,
                persistedObject.GetProperty("UpdatedDate").GetDateTimeOffset().DateTime
            );
        
        // loader / builder
        var product = ProductLoader.CreateLoader()
            .WithId(persistedObject.GetProperty("Id").GetGuid())
            .WithName(persistedObject.GetProperty("Name").GetString()!)
            .WithDescription(persistedObject.GetProperty("Description").GetString()!)
            .WithStatus((Status)persistedObject.GetProperty("Status").GetInt32())
            .WithPrice(persistedObject.GetProperty("Price").GetDecimal())
            .WithCreatedDate(persistedObject.GetProperty("CreatedDate").GetDateTimeOffset().DateTime)
            .WithUpdatedDate(persistedObject.GetProperty("UpdatedDate").GetDateTimeOffset().DateTime)
            .WithQuantityInStock(persistedObject.GetProperty("QuantityInStock").GetInt32())
            .Load();
        
        return Task.FromResult<Product?>(product);
    }

    public Task<List<Product>> GetAll(CancellationToken cancellationToken) => 
        Task.FromResult<List<Product>>(Enumerable.Empty<Product>().ToList());
}