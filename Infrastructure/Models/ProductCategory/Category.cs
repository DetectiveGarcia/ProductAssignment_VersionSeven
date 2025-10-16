using Infrastructure.Helpers;
using System.Text.Json.Serialization;

namespace Infrastructure.Models.ProductCategory;
public class Category
{
    [JsonInclude]
    public string Id { get; private set; }
    public string Name { get; set; } = null!;


    public Category()
    {
        if (UniqueIdentifierGenerator.Generate() == Guid.Empty)
        {
            Id = "Missing ID"; //vill man göra något med dessa MissingIDs???
            return;
        }

        Id = $"C-{UniqueIdentifierGenerator.Generate()}";
    }
}
