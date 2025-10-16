using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Models.Product;
using Infrastructure.Models.ProductCategory;
using Infrastructure.Models.ProductManufacture;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.WpfApp.ViewModels;

public partial class UpdateProductViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductManager _productManager;


    public UpdateProductViewModel(IServiceProvider serviceProvider, IProductManager productManager)
    {
        _serviceProvider = serviceProvider;
        _productManager = productManager;
    }

    [ObservableProperty]
    private string _header = "Update a product";

    [ObservableProperty]
    private string _productId;

    [ObservableProperty]
    private UpdateProductRequest _updateProductRequest = new()
    {
        Category = new UpdateCategoryRequest(),
        Manufacture = new UpdateManufactureRequest()
    };

    [RelayCommand]
    private void GoToMainMenu()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainMenuViewModel>();
    }

    [RelayCommand]
    private async Task GetProductByIdBtn()
    {
        var product = await _productManager.GetProductById(ProductId);

        UpdateProductRequest = new UpdateProductRequest
        {
            Name = product.Content.Name,
            Description = product.Content.Description,
            Price = product.Content.Price.ToString(),
            Category = new UpdateCategoryRequest { Name = product.Content.Category.Name },
            Manufacture = new UpdateManufactureRequest { Name = product.Content.Manufacture.Name }
        };

    }

    [RelayCommand]
    private async Task UpdateProduct()
    {
        await _productManager.UpdateProductAsync(ProductId, UpdateProductRequest.Name, UpdateProductRequest.Price, UpdateProductRequest.Category.Name, UpdateProductRequest.Manufacture.Name, UpdateProductRequest.Description);

        UpdateProductRequest = new()
        {
            Category = new UpdateCategoryRequest(),
            Manufacture = new UpdateManufactureRequest()
        };
    }

}
