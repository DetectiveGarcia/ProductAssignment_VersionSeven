using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.WpfApp.ViewModels;
using Presentation.WpfApp.Views;
using Infrastructure.Interfaces;
using System.Windows;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Managers;

namespace Presentation.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddSingleton<MainMenuViewModel>();
                services.AddSingleton<MainMenuView>();

                services.AddSingleton<AddProductViewModel>();
                services.AddSingleton<AddProductView>();

                services.AddSingleton<UpdateProductViewModel>();
                services.AddSingleton<UpdateProductView>();

                services.AddSingleton<DeleteProductViewModel>();
                services.AddSingleton<DeleteProductView>();

                services.AddTransient<ProductListViewModel>();
                services.AddSingleton<ProductListView>();

                services.AddSingleton<IJsonFileRepository>(_ => new JsonFileRepository());
                services.AddSingleton<IProductService, ProductService>();
                services.AddSingleton<IProductManager, ProductManager>();

            })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

    }

}
