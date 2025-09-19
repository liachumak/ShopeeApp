using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopeeApp.Models.Product;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    // Foreign key
    public int CategoryId { get; set; }

    // Navigation
    public ShopeeApp.Models.Category.Category Category { get; set; }

    public List<ProductImage> Images { get; set; }
}
