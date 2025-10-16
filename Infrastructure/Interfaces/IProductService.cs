using Infrastructure.Models.Product;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    Task<ProductObjectResult<IReadOnlyList<Product>>> GetProductsAsync(CancellationToken cancellationToken = default);
    Task<ProductObjectResult<Product>> GetProductByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<ProductObjectResult<Product>> GetProductByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<ProductResult> SaveProductAsync(CreateProductRequest product, CancellationToken cancellationToken = default);
    Task<ProductResult> UpdateProductAsync(UpdateProductRequest product, CancellationToken cancellationToken = default);
    Task<ProductResult> DeleteProductAsync(DeleteProductRequest product, CancellationToken cancellationToken = default);
}
