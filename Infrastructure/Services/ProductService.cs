using Infrastructure.Interfaces;
using Infrastructure.Models.Product;
using Infrastructure.Models.ProductCategory;
using Infrastructure.Models.ProductManufacture;

namespace Infrastructure.Services;

public class ProductService(IJsonFileRepository jsonFileRepository) : IProductService
{
    private readonly IJsonFileRepository _jsonFileRepository = jsonFileRepository;
    private List<Product> _productList = [];
    private bool _loaded;

    public async Task EnsureLoadedAsync(CancellationToken cancellationToken = default)
    {
        if (_loaded) return;
        var result = await _jsonFileRepository.ReadAsync(cancellationToken);
        _productList = [.. result];

        _loaded = true;
    }

    public async Task<ProductResult> SaveProductAsync(CreateProductRequest newProduct, CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync(cancellationToken);

        if (newProduct == null) 
            return new ProductResult { Success = false, StatusCode = 400, Error = "Product was null" };

        try
        {
            Category category = new()
            {
                Name = newProduct.Category.Name
            };

            Manufacture manufacture = new()
            {
                Name = newProduct.Manufacture.Name
            };

            Product product = new()
            {
                Name = newProduct.Name,
                Price = decimal.Parse(newProduct.Price),
                Manufacture = manufacture,
                Description = newProduct.Description ?? "No description",
                Category = category
            };


            if (product.Id == "Missing ID")
                return new ProductResult()
                {
                    Success = false,
                    Error = "Something went wrong when creating ID",
                    StatusCode = 400
                };

            var products = await _jsonFileRepository.ReadAsync(cancellationToken);

            var doubleProduct = products.Any(p => product.Name == p.Name);

            if (doubleProduct)
                return new ProductResult()
                {
                    Success = false,
                    Error = "Product already exists",
                    StatusCode = 422
                };

            _productList.Add(product);


            await _jsonFileRepository.WriteAsync(_productList, cancellationToken);

            return new ProductResult()
            {
                Success = true,
                StatusCode = 200,
                Message = "Product created."
            };


        }catch(Exception ex)
        {
            return new ProductResult()
            {
                Success = false,
                Error = ex.Message,
                StatusCode = 500
            };
        }
        
    }

    public async Task<ProductObjectResult<IReadOnlyList<Product>>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync(cancellationToken);
        var products = await _jsonFileRepository.ReadAsync(cancellationToken); //Kan man ta bort denna oh använda bara _productList? 

        if (products.Count == 0 || products == null)
            return new ProductObjectResult<IReadOnlyList<Product>>()
            {
                Success = false,
                StatusCode = 404,
                Error = "No products found",
                Content = []
            };

        return new ProductObjectResult<IReadOnlyList<Product>>()
        {
            //Content = products,
            Content = _productList,
            StatusCode = 200,
            Success = true
        };
    }

    public async Task<ProductObjectResult<Product>> GetProductByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync(cancellationToken);
        var product = _productList.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return new ProductObjectResult<Product>
            {
                Success = false,
                Error = "Product not found",
                StatusCode = 404,
            };

        return new ProductObjectResult<Product>()
        {
            Content = product,
            StatusCode = 200,
            Success = true,
        };
        
    }

    public async Task<ProductObjectResult<Product>> GetProductByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync(cancellationToken);
        var product = _productList.FirstOrDefault(p => p.Name == name);

        if (product == null)
            return new ProductObjectResult<Product>
            {
                Success = false,
                Error = "Product not found",
                StatusCode = 404,
            };

        return new ProductObjectResult<Product>()
        {
            Content = product,
            StatusCode = 200,
            Success = true,
        };

    }


    public async Task<ProductResult> UpdateProductAsync(UpdateProductRequest product, CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync();

        var productToUpdate = _productList.FirstOrDefault(p => p.Id == product.Id);





        if (productToUpdate == null)
            return new ProductResult()
            {
                Success = false,
                StatusCode = 404,
                Error = "Product not found"
            };



        productToUpdate.Name = product.Name;
        productToUpdate.Description = product.Description ?? "No description";
        productToUpdate.Price = decimal.Parse(product.Price);
        productToUpdate.Category.Name = product.Category.Name;
        productToUpdate.Manufacture.Name = product.Manufacture.Name;




        
         await _jsonFileRepository.WriteAsync(_productList, cancellationToken);

        return new ProductResult()
        {
            Success = true,
            StatusCode = 201,
            Message = "Product updated."
        };
    }


    public async Task<ProductResult> DeleteProductAsync(DeleteProductRequest productId, CancellationToken cancellationToken = default)
    {
        await EnsureLoadedAsync(cancellationToken);

        var productForDeletion = _productList.FirstOrDefault(p => p.Id == productId.Id);

        if (productForDeletion == null)
            return new ProductResult()
            {
                StatusCode = 404,
                Success = false,
                Error = "Product not found, can't delete product."
            };

        _productList.Remove(productForDeletion);

        await _jsonFileRepository.WriteAsync(_productList, cancellationToken);

/*        var productResult = await _jsonFileRepository.DeleteAsync(productId.Id, cancellationToken)*/;

        return new ProductResult()
        {
            Success = true,
            StatusCode = 204,
            Message = "Product deleted"
        };

    }

}
