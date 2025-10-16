using Infrastructure.Models.ProductCategory;
using Infrastructure.Models.ProductManufacture;

namespace Infrastructure.Models.Product;
public class UpdateProductRequest
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string Price { get; set; } = null!;
    public UpdateCategoryRequest Category { get; set; } = null!;
    public UpdateManufactureRequest Manufacture { get; set; } = null!;
}
