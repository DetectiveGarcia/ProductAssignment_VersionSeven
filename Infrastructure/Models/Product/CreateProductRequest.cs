using Infrastructure.Models.ProductCategory;
using Infrastructure.Models.ProductManufacture;
namespace Infrastructure.Models.Product;

public class CreateProductRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Price { get; set; } = null!;
    public CreateCategoryRequest Category { get; set; } = null!;
    public CreateManufactureRequest Manufacture { get; set; } = null!;
}