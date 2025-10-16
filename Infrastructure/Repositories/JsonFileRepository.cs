using Infrastructure.Interfaces;
using Infrastructure.Models.Product;
using System.Text.Json;

namespace Infrastructure.Repositories;

public class JsonFileRepository : IJsonFileRepository
{
    private readonly string _filePath;
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    public JsonFileRepository(string filePath = "data.json")
    {
        var baseDirectory = AppContext.BaseDirectory;
        var dataDirectory = Path.Combine(baseDirectory, "Data");
        _filePath = Path.Combine(dataDirectory, filePath);
        EnsureInitialized(dataDirectory, _filePath);
    }

    //Försäkra sig att det finns en fil.
    public static void EnsureInitialized(string dataDirectory, string filePath)
    {
        if (!Directory.Exists(dataDirectory))
            Directory.CreateDirectory(dataDirectory);

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "[]");
    }

    public async Task WriteAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default)
    {

        //Var gör using här? och varför ha Create här när vi ska redan ha en fil (pga EnsureInitialized())? 
        await using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, products, _jsonOptions, cancellationToken);
    }

    public async ValueTask<IReadOnlyList<Product>> ReadAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await using var stream = File.OpenRead(_filePath);
            var products = await JsonSerializer.DeserializeAsync<List<Product>>(stream, _jsonOptions, cancellationToken);
            return products ?? [];
        }
        catch
        {
            return [];
        }
    }

    
}
