using System.ComponentModel.DataAnnotations;
using ShopeeApp.ViewModels;

namespace ShopeeApp.ViewModels
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public List<IFormFile>? NewImages { get; set; }

        public List<ProductImageViewModel> ExistingImages { get; set; } = new();
    }

}

