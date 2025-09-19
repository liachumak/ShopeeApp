using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopeeApp.Models.Product;

namespace ShopeeApp.Models.Category;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public List<ProductImage> Products { get; set; }
}
