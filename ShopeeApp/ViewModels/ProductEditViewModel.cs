using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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

   /* public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int Priority { get; set; }
    }*/
}

