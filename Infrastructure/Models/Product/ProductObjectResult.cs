namespace Infrastructure.Models.Product;

public class ProductObjectResult<T> : ProductResult
{
    public T? Content { get; set; }
}