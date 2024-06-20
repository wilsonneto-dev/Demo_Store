using System.Dynamic;

namespace Catalog.Domain.ProductAggregate.Loaders;

public class ProductLoader
{
    private string _name = string.Empty;
    private decimal _price = 0;
    private string _description = string.Empty;
    private int _quantityInStock = 0;
    private Status _status;
    private Guid _id = Guid.Empty;
    private DateTime _createdDate = DateTime.Now;
    private DateTime _updatedAt = DateTime.Now;

    private ProductLoader()
    {
    }

    public static ProductLoader CreateLoader() => new ProductLoader();

    public ProductLoader WithId(Guid id)
    {
        _id = id;
        return this;
    }
    
    public ProductLoader WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProductLoader WithPrice(decimal price)
    {
        _price = price;
        return this;
    }

    public ProductLoader WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ProductLoader WithQuantityInStock(int quantityInStock)
    {
        _quantityInStock = quantityInStock;
        return this;
    }

    public ProductLoader WithStatus(Status status)
    {
        _status = status;
        return this;
    }

    public ProductLoader WithCreatedDate(DateTime createdDate)
    {
        _createdDate = createdDate;
        return this;
    }
    public ProductLoader WithUpdatedDate(DateTime updatedDate)
    {
        _updatedAt = updatedDate;
        return this;
    }

    public Product Load() => Product.Load(
        _id,
        _name,
        _description,
        _price,
        _quantityInStock,
        _status,
        _createdDate,
        _updatedAt);
}
