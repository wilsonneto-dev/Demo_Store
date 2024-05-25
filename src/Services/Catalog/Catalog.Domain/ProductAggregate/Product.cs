using Domain.SeedWork;
using Domain.SeedWork.Exceptions;
using Domain.SeedWork.Validations;

namespace Catalog.Domain.ProductAggregate;

public sealed class Product : AggregateRoot
{
    public string Name { get; private set; } = "";
    public decimal Price { get; private set; } = 0;
    public string Description { get; private set; } = "";

    public Product() {}

    public Product(string name, decimal price, string description)
    {
        Name = name;
        Price = price;
        Description = description;
        RaiseEvent(new ProductCreated(Id, Name, Price));
        Validate();
    }

    public void Update(string name, decimal price, string description)
    {
        Name = name;
        Price = price;
        Description = description;
        RaiseEvent(new ProductUpdated(Id, Name, price));
        Validate();
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
}