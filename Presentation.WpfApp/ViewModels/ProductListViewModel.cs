using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Models.Product;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Presentation.WpfApp.ViewModels;

public partial class ProductListViewModel : ObservableObject
{

    private readonly IServiceProvider _serviceProvider;
    private readonly IProductManager _productManager;
    private IAsyncRelayCommand LoadCommand { get; }
    [ObservableProperty]
    private ObservableCollection<Product> _products;

    public ProductListViewModel(IServiceProvider serviceProvider, IProductManager productManager)
    {
        _serviceProvider = serviceProvider;
        _productManager = productManager;
        LoadCommand = new AsyncRelayCommand(LoadProductListAsync);
        _ = LoadCommand.ExecuteAsync(null);
    }

    [ObservableProperty]
    private string _header = "List of products";




    [RelayCommand]
    private void GoToMainMenu()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainMenuViewModel>();
    }

    private async Task LoadProductListAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productManager.GetProductList();
        if(products.Content == null)
        {
            Products = [];
            return;
        }

        Products = [.. products.Content];
    }
}
