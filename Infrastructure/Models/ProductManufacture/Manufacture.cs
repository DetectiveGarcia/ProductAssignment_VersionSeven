using Infrastructure.Helpers;
using System.Text.Json.Serialization;

namespace Infrastructure.Models.ProductManufacture;
public class Manufacture
{
    [JsonInclude]
    public string Id { get; private set; }
    public string Name { get; set; } = null!;

    public Manufacture()
    {
        if (UniqueIdentifierGenerator.Generate() == Guid.Empty)
        {
            Id = "Missing ID"; //vill man göra något med dessa MissingIDs???
            return;
        }

        Id = $"M-{UniqueIdentifierGenerator.Generate()}";
    }

}
