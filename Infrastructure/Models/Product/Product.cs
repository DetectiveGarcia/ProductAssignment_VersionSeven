using Infrastructure.Helpers;
using System.Text.Json.Serialization;
using Infrastructure.Models.ProductCategory;
using Infrastructure.Models.ProductManufacture;

namespace Infrastructure.Models.Product;

public class Product
{
    [JsonInclude]
    public string Id { get; private set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public Category Category { get; set; } = null!;
    public Manufacture Manufacture { get; set; } = null!;

    public Product()
    {
        if(UniqueIdentifierGenerator.Generate() == Guid.Empty)
        {
            Id = "Missing ID"; //vill man göra något med dessa MissingIDs???
            return;
        }

        Id = $"P-{UniqueIdentifierGenerator.Generate()}";

        

    }
}
