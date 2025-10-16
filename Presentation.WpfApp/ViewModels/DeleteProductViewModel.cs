using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Models.Product;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.WpfApp.ViewModels;

public partial class DeleteProductViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductManager _productManager;
    public DeleteProductViewModel(IServiceProvider serviceProvider, IProductManager productManager)
    {
        _serviceProvider = serviceProvider;
        _productManager = productManager;
    }

    [ObservableProperty]
    private string _header = "Delete a product";

    [ObservableProperty]
    private DeleteProductRequest _productId = new(); 

    [RelayCommand]
    private void DeleteProduct()
    {
        var result = _productManager.DeleteProductAsync(ProductId.Id);
        ProductId = new DeleteProductRequest();
    }

    [RelayCommand]
    private void GoToMainMenu()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainMenuViewModel>();
    }



}
