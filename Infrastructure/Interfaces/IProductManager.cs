using Infrastructure.Models.Product;

namespace Infrastructure.Interfaces;

public interface IProductManager
{
    Task<string> CreateProduct(string name, string price, string category, string manufacture, string? description);
    Task<ProductObjectResult<IReadOnlyList<Product>>> GetProductList();
    Task<ProductObjectResult<Product>> GetProductById(string productId);
    Task<ProductObjectResult<Product>> GetProductByName(string productName);
    Task<ProductResult> UpdateProductAsync(string productId, string name, string price, string category, string manufacture, string? description);
    Task<ProductResult> DeleteProductAsync(string productId);

}
