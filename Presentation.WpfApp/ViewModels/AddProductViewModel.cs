using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Models.Product;
using Microsoft.Extensions.DependencyInjection;


namespace Presentation.WpfApp.ViewModels;

public partial class AddProductViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductManager _productManager;
    public AddProductViewModel(IServiceProvider serviceProvider, IProductManager productManager)
    {
        _serviceProvider = serviceProvider;
        _productManager = productManager;

    }

    [ObservableProperty]
    private string _header = "Create a product";

    [ObservableProperty]
    private CreateProductRequest _productRequest = new()
    {
        Category = new(),
        Manufacture = new()
    };


    [RelayCommand]
    private void GoToMainMenu()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainMenuViewModel>();
    }

    [RelayCommand]
    private async Task AddProduct()
    {
        var result = await _productManager.CreateProduct(ProductRequest.Name, ProductRequest.Price, ProductRequest.Category.Name, ProductRequest.Manufacture.Name, ProductRequest.Description);
        ProductRequest = new CreateProductRequest
        {
            Category = new(),
            Manufacture = new()
        };
    }
}
