using System.Text.Json.Serialization;
using Domain.SeedWork;
using Domain.SeedWork.Exceptions;
using Domain.SeedWork.Validations;
using Newtonsoft.Json;

namespace Catalog.Domain.ProductAggregate;

public sealed class Product : AggregateRoot
{
    public string Name { get; private set; } = "";
    public decimal Price { get; private set; } = 0;
    public string Description { get; private set; } = "";

    [JsonProperty]
    [JsonInclude]
    public int QuantityInStock { get; private set; }

    [JsonProperty]
    [JsonInclude]
    public Status Status { get; private set; }

    [JsonProperty]
    [JsonInclude]
    public DateTime CreatedDate { get; private set; }

    [JsonProperty]
    [JsonInclude]
    public DateTime UpdatedDate { get; private set; }

    public Product(string name, decimal price, string description)
    {
        Name = name;
        Price = price;
        Description = description;

        QuantityInStock = 0;
        CreatedDate = DateTime.Now;
        UpdatedDate = DateTime.Now;

        RaiseEvent(new ProductCreated(Id, Name, Price));
        Validate();
    }

    public void Update(string name, decimal price, string description)
    {
        Name = name;
        Price = price;
        Description = description;
        UpdatedDate = DateTime.Now;
        RaiseEvent(new ProductUpdated(Id, Name, price));
        Validate();
    }

    public void Activate()
    {
        UpdatedDate = DateTime.Now;
        Status = Status.Active;
    }

    public void Deactivate()
    {
        UpdatedDate = DateTime.Now;
        Status = Status.Disabled;
    }

    public void AddStock(int quantity)
    {
        UpdatedDate = DateTime.Now;
        QuantityInStock += quantity;
    }

    public void RemoveStock(int quantity)
    {
        UpdatedDate = DateTime.Now;
        QuantityInStock -= quantity;
    }

    private void Validate()
    {
        var notificationHandler = new NotificationValidationHandler();
        DomainValidation.NotNullOrEmpty(Name, nameof(Name), notificationHandler);
        DomainValidation.MinLength(Name, 10, nameof(Name), notificationHandler);
        DomainValidation.NotNull(Description, nameof(Description), notificationHandler);
        DomainValidation.MinLength(Description, 20, nameof(Description), notificationHandler);
        if (notificationHandler.HasErrors())
            throw new EntityValidationException(
                "Product validation failed", notificationHandler.Errors);
    }
    
    public static Product Load(
        Guid id,
        string name,
        string description,
        decimal price,
        int quantityInStock,
        Status status,
        DateTime createdDate,
        DateTime updatedAt
    )
    {
        return new Product(name, price, description)
        {
            Id = id,
            QuantityInStock = quantityInStock,
            Status = status,
            CreatedDate = createdDate,
            UpdatedDate = updatedAt,
        };
        
        // Validation(); - possible post validation
    }
}    

