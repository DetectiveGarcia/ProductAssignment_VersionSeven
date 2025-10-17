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
    private string _productId = null!;

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
        UpdateProductRequest = new()
        {
            Category = new UpdateCategoryRequest(),
            Manufacture = new UpdateManufactureRequest()
        };
        ProductId = string.Empty;
    }

    [RelayCommand]
    private async Task GetProductByIdBtn()
    {
        var result = await _productManager.GetProductById(ProductId);

        if (!result.Success)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

            var updateProductVM = _serviceProvider.GetRequiredService<UpdateProductViewModel>();

            var notificationVM = new NotificationViewModel(_serviceProvider, result.Message ?? result.Error, result.Success, updateProductVM);

            mainViewModel.CurrentViewModel = notificationVM;
            return;
        }

  

        UpdateProductRequest = new UpdateProductRequest
        {
            Name = result.Content.Name,
            Description = result.Content.Description,
            Price = result.Content.Price.ToString(),
            Category = new UpdateCategoryRequest { Name = result.Content.Category.Name },
            Manufacture = new UpdateManufactureRequest { Name = result.Content.Manufacture.Name }
        };



    }

    [RelayCommand]
    private async Task UpdateProduct()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();

        var result = await _productManager.UpdateProductAsync(ProductId, UpdateProductRequest.Name, UpdateProductRequest.Price, UpdateProductRequest.Category.Name, UpdateProductRequest.Manufacture.Name, UpdateProductRequest.Description);

        var updateProductVM = _serviceProvider.GetRequiredService<UpdateProductViewModel>();

        var notificationVM = new NotificationViewModel(_serviceProvider, result.Message ?? result.Error, result.Success, updateProductVM);

        mainViewModel.CurrentViewModel = notificationVM;

        if (!result.Success) return;

        UpdateProductRequest = new()
        {
            Category = new UpdateCategoryRequest(),
            Manufacture = new UpdateManufactureRequest()
        };
    }

}
