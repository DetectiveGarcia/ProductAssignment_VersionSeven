using Infrastructure.Interfaces;
using Infrastructure.Managers;
using Infrastructure.Models.Product;
using Infrastructure.Services;
using Moq;

namespace Infrastructure.Tests.Services;


/// <summary>
/// Använde Hans videos för constructor delen och chatGPT för _jsonFileRepositoryMock setup delen.
/// </summary>
public class ProductService_Tests
{
    private Mock<IJsonFileRepository> _jsonFileRepositoryMock;
    private IJsonFileRepository _jsonFileRepository;
    private IProductService _productService;
    private IProductManager _productManager;

    public ProductService_Tests()
    {
        _jsonFileRepositoryMock = new();
        _jsonFileRepository = _jsonFileRepositoryMock.Object;
        _productService = new ProductService(_jsonFileRepository);
        _productManager = new ProductManager(_productService);
    }

    [Fact]
    public async Task SaveProductAsync_ShouldReturnTrueProductResult_WhenSavingProduct()
    {
        //Arrange

        _jsonFileRepositoryMock.Setup(repo => repo.ReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        _jsonFileRepositoryMock.Setup(repo => repo.WriteAsync(It.IsAny<IEnumerable<Product>>(), It.IsAny<CancellationToken>()));

        string name = "Frozen Vegetarian Pizza";
        string price = "6,49";
        string category = "Food - Frozen Meals";
        string manufacture = "GreenOven Foods";
        string description = "A delicious pizza loaded with vegetables";

        //Act

        var productResult = await _productManager.CreateProduct(name, price, category, manufacture, description);

        //Assert
        Assert.Equal("Product created.", productResult);
    }

}
