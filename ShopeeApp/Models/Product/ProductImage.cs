namespace ShopeeApp.Models.Product;

public class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public string FileName { get; set; }

    public int Priority { get; set; } // 1, 2, 3...
}
