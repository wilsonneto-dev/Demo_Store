using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Catalog.Domain.ProductAggregate;

namespace Catalog.Data.InMemoryJson;

public class ProductJsonConverter : JsonConverter<Product>
{
    private readonly Dictionary<string, Type> _properties;

    public ProductJsonConverter(Dictionary<string, Type> properties)
    {
        _properties = properties;
    }

    public override Product Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var product = (Product)Activator.CreateInstance(typeof(Product), true);

        var properties = typeof(Product).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return product;
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString();
                reader.Read();

                if (_properties.TryGetValue(propertyName, out Type propertyType))
                {
                    var property = Array.Find(properties, p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
                    if (property != null)
                    {
                        var value = JsonSerializer.Deserialize(ref reader, propertyType, options);
                        property.SetValue(product, value);
                    }
                }
            }
        }

        throw new JsonException("Invalid JSON for Product");
    }

    public override void Write(Utf8JsonWriter writer, Product product, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        var properties = typeof(Product).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            if (_properties.ContainsKey(property.Name))
            {
                var value = property.GetValue(product);
                writer.WritePropertyName(property.Name);
                JsonSerializer.Serialize(writer, value, property.PropertyType, options);
            }
        }

        writer.WriteEndObject();
    }
}