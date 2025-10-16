using Infrastructure.Interfaces;
using Infrastructure.Models.Product;
using Infrastructure.Models.ProductCategory;
using Infrastructure.Models.ProductManufacture;

namespace Infrastructure.Managers;

public class ProductManager(IProductService productService) : IProductManager
{
    private readonly IProductService _productService = productService;
    private CancellationTokenSource _cts = new();
    private bool _loading = false;


    private void Cancel()
    {
        _cts.Cancel();
    }

    public async Task<string> CreateProduct(string name, string price, string category, string manufacture, string? description)
    {
        if (_loading)
        {
            Cancel();
        }
        _cts = new();
        var parsedPrice = decimal.Parse(price);
        if (parsedPrice <= 0)
            return "Price must be over 0 money units";

        CreateCategoryRequest categoryRequest = new()
        {
            Name = category
        };

        CreateManufactureRequest manufactureRequest = new()
        {
            Name = manufacture
        };

        CreateProductRequest newProduct = new()
        {
            Name = name,
            Price = price,
            Category = categoryRequest,
            Manufacture = manufactureRequest,
            Description = description
        };

        var productResult = await _productService.SaveProductAsync(newProduct, _cts.Token);

        if (!productResult.Success)
        {
            return productResult.Error;
        }

        return productResult.Message;
    }

    public async Task<ProductObjectResult<IReadOnlyList<Product>>> GetProductList()
    {
        _cts = new();
        if (_loading)
        {
            Cancel();
        }

        var productResult = await _productService.GetProductsAsync(_cts.Token);

        return productResult;

    }

    public async Task<ProductObjectResult<Product>> GetProductById(string productId)
    {
        _cts = new();
        if (_loading)
        {
            Cancel();
        }
        var productResult = await _productService.GetProductByIdAsync(productId, _cts.Token);

        if (!productResult.Success)
            return productResult;

        return productResult;
    }

    public async Task<ProductObjectResult<Product>> GetProductByName(string productName)
    {
        _cts = new();
        if (_loading)
        {
            Cancel();
        }
        var productResult = await _productService.GetProductByNameAsync(productName, _cts.Token);

        if (!productResult.Success)
            return productResult;

        return productResult;
    }

    public async Task<ProductResult> UpdateProductAsync(string productId, string name, string price, string category, string manufacture, string? description)
    {
        _cts = new();
        if (_loading)
        {
            Cancel();
        }

        UpdateCategoryRequest updateCategoryRequest = new()
        {
            Name = category
        };

        UpdateManufactureRequest updateManufactureRequest = new()
        {
            Name = manufacture
        };

        UpdateProductRequest updateProductRequest = new()
        {
            Id = productId,
            Name = name,
            Description = description,
            Price = price,
            Category = updateCategoryRequest,
            Manufacture = updateManufactureRequest
        };

        var productResult = await _productService.UpdateProductAsync(updateProductRequest, _cts.Token);

        return productResult;


    }

    public async Task<ProductResult> DeleteProductAsync(string productId)
    {
        _cts = new();
        if (_loading)
        {
            Cancel();
        }

        DeleteProductRequest productForDeletion = new()
        {
            Id = productId
        };

        var productResult = await _productService.DeleteProductAsync(productForDeletion, _cts.Token);

        if (!productResult.Success)
            return new ProductResult()
            {
                StatusCode = productResult.StatusCode,
                Error = productResult.Error,
                Success = productResult.Success
            };

        return new ProductResult()
        {
            Success = productResult.Success,
            Error = productResult.Error,
            StatusCode = productResult.StatusCode
        };

    }

}
