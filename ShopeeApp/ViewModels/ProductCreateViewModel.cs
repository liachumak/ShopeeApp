
using System.ComponentModel.DataAnnotations;

namespace ShopeeApp.ViewModels;

public class ProductCreateViewModel
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public List<IFormFile> Images { get; set; } = new();

    public List<ProductImageViewModel> ExistingImages { get; set; } = new List<ProductImageViewModel>();
}
public class ProductImageViewModel
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public int Priority { get; set; }
}
