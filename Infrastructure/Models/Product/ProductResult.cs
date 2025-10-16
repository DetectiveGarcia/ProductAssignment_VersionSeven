namespace Infrastructure.Models.Product;

public class ProductResult
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public string Error { get; set; } = null!;
    public string Message { get; set; } = null!;
}
