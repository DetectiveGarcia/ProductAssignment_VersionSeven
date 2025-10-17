using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Models.Product;
using System.Transactions;

namespace Presentation.ConsoleApp;
public class MenuDialogs(IProductManager productManager)
{

    private readonly IProductManager _productManager = productManager;

    public List<string> _menuOptions = ["Create product", "Find product by ID", "Find product by name", "View products", "Update product", "Delete product"];
    public async Task Run()
    {
        await MainMenu();
    }

    private async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("#### Main menu ####");
            Console.WriteLine("");
            var menuOptions = _menuOptions.Select((option, index) => $"{index + 1}. {option}");
            foreach (var menuOption in menuOptions)
                Console.WriteLine(menuOption);
            Console.WriteLine("0. Exit");
            var option = Prompt.DisplayAndRead("Enter your option: ");

            switch (option)
            {
                case "1":
                    await CreateProductOption();
                    break;
                case "2":
                    await FindProductByIdOption();
                    break;
                case "3":
                    await FindProductByNameOption();
                    break;
                case "4":
                    await ProductListOption();
                    break;
                case "5":
                    await UpdateProductOption();
                    break;
                case "6":
                    await DeleteProductOption();
                    break;
                case "0":
                    Exit();
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
            Console.ReadKey();
        }
    }

    private async Task CreateProductOption()
    {
        Console.Clear();
        Console.WriteLine("#### Create new product ####");
        Console.WriteLine("");

        string name = Prompt.DisplayAndRead("Enter product name: ");
        string description = Prompt.DisplayAndRead("Enter product description: ");
        string price = Prompt.DisplayAndRead("Enter product price: ");
        string category = Prompt.DisplayAndRead("Enter product category: ");
        string manufacture = Prompt.DisplayAndRead("Enter product manufacture: ");

        var productResult = await _productManager.CreateProduct(name, price, category, manufacture, description);

        if (!productResult.Success)
        {
            Console.WriteLine(productResult.Error);
            return;
        }

        Console.WriteLine(productResult.Message);
    }

    private async Task FindProductByIdOption()
    {
        Console.Clear();
        Console.WriteLine("#### Find product by ID ####");
        Console.WriteLine("");

        var productId = Prompt.DisplayAndRead("Enter product id: ");

        var productResult = await _productManager.GetProductById(productId);

        if (!productResult.Success || productResult.Content == null)
        {
            Console.WriteLine(productResult.Error);
            return;
        }

        var product = productResult.Content;

        Console.WriteLine(
            $"Product ID: {product.Id}" +
            $"\n Product name: {product.Name}" +
            $"\n Product description: {product.Description}" +
            $"\n Product price: {product.Price}" +
            $"\n Product category: {product.Category.Name}" +
            $"\n Product manufacture: {product.Manufacture.Name}"
            );
    }

    private async Task FindProductByNameOption()
    {
        Console.Clear();
        Console.WriteLine("#### Find product by name ####");
        Console.WriteLine("");

        var productName = Prompt.DisplayAndRead("Enter product name: ");

        var productResult = await _productManager.GetProductByName(productName);

        if (!productResult.Success || productResult.Content == null)
        {
            Console.WriteLine(productResult.Error);
            return;
        }

        var product = productResult.Content;

        Console.WriteLine(
            $"ID: {product.Id}" +
            $"\n Name: {product.Name}" +
            $"\n Description: {product.Description}" +
            $"\n Price: {product.Price}" +
            $"\n Category: {product.Category.Name}" +
            $"\n Manufacture: {product.Manufacture.Name}"
            );

    }

    private async Task ProductListOption()
    {
        Console.Clear();
        Console.WriteLine("#### Product list ####");
        Console.WriteLine("");
        var productList = await _productManager.GetProductList();

        if(!productList.Success || productList.Content == null)//kolla om content är null i GetProductsAsync() i ProductService också - Följer inte DRY??
        {
            Console.WriteLine(productList.Error);
            return;
        }


        
        foreach (var productItem in productList.Content)
        {
            Console.WriteLine("");
            Console.WriteLine($"ID: {productItem.Id}" +
            $"\n Name: {productItem.Name}" +
            $"\n Description: {productItem.Description}" +
            $"\n Price: ${productItem.Price}" +
            $"\n Category: {productItem.Category.Name}" +
            $"\n Manufacture: {productItem.Manufacture.Name}");

            Console.WriteLine("");
            Console.WriteLine("----------------------");

        }
    }

    private async Task DeleteProductOption()
    {
        Console.Clear();
        Console.WriteLine("#### Delete product ####");
        Console.WriteLine("");
        string productId = Prompt.DisplayAndRead("Enter product id for deletion: ");
        var result = await _productManager.DeleteProductAsync(productId);

        if (!result.Success)
        {
            Console.WriteLine(result.Error);
            return;

        }
        Console.WriteLine(result.Message);
     
    }

    private async Task UpdateProductOption()
    {
        Console.Clear();
        Console.WriteLine("#### Update product ####");
        Console.WriteLine("");
        string productId = Prompt.DisplayAndRead("Enter product ID you want to update: ");

        var product = await _productManager.GetProductById(productId);

        if (!product.Success || product.Content == null)
        {
            Console.WriteLine(product.Error);
            return;
        }


        string name =           Prompt.DisplayAndReadUpdate("Enter new name: ", product.Content.Name);
        string description =    Prompt.DisplayAndReadUpdate("Enter new description: ", product.Content.Description);
        string price =          Prompt.DisplayAndReadUpdate("Enter new price: ", product.Content.Price.ToString());
        string category =       Prompt.DisplayAndReadUpdate("Enter new categroy: ", product.Content.Category.Name);
        string manufacture =    Prompt.DisplayAndReadUpdate("Enter new manufacture: ", product.Content.Manufacture.Name);


        var productResult = await _productManager.UpdateProductAsync(productId, name, price, category, manufacture, description);

        if (!productResult.Success)
        {
            Console.WriteLine(productResult.Error);
            return;
        }

        Console.WriteLine(productResult.Message);

    }

    private static void Exit()
    {
        Environment.Exit(0);
    }
}
