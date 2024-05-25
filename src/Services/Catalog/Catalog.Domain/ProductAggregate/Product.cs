namespace Catalog.Domain.ProductAggregate;

public class Product
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    
    public Product(string name, decimal price, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Description = description;
        
        Validate();
    }

    public void Update(string name, decimal price, string description)
    {
        Name = name;
        Price = price;
        Description = description;
    }

    private static void Validate()
    {
    }
}