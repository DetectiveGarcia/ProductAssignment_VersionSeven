using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;


namespace Presentation.WpfApp.ViewModels;
public partial class MainMenuViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public MainMenuViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [ObservableProperty]
    private string _header = "Main menu";

    [RelayCommand]
    private void GoToAddProduct()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddProductViewModel>();
    }


    [RelayCommand]
    private void GoToUpdateProduct()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UpdateProductViewModel>();
    }

    [RelayCommand]
    private void GoToDeleteProduct()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DeleteProductViewModel>();
    }

    [RelayCommand]
    private void GoToProductList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ProductListViewModel>();
    }

    [RelayCommand]
    private static void Exit()
    {
        Environment.Exit(0);
    }
}
