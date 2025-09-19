using Microsoft.EntityFrameworkCore;
using ShopeeApp.Models.Category;
using ShopeeApp.Models.Product;

namespace ShopeeApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Телефони" },
            new Category { Id = 2, Name = "Ноутбуки" }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "iPhone 17 Pro Max APPLE (6.9'' - 256 GB)", Description = "Новий iPhone! Чомусь мій мобільний оператор відправив мені повідомлення з пропозицією купити його на прередпродажі.", Price = 14999.00m, CategoryId = 1 },
            new Product { Id = 2, Name = "Smartphone SAMSUNG Galaxy S25 FE (6.7'' - 8 GB - 512 GB)", Description = "Новий SAMSUNG. Це топ, це класика.", Price = 1082.10m, CategoryId = 1 },
            new Product { Id = 3, Name = "Smartphone OPPO Reno 14 (6.59'' - 12 GB - 512 GB)", Description = "Новий OPPO смартфон допоможе тобі бути в тренді, виконувати необхідні функції та працювати в своє задоволення", Price = 649.99m, CategoryId = 1 },
            new Product { Id = 4, Name = "Portátil HP Laptop 15-fd0019np (15.6\" - Intel Core i3 N305 - RAM:8 GB - 256 GB SSD - Intel UHD)", Description = "Потужний ноутбук. Взагалі ХР дуже популярні та зручні", Price = 549.99m, CategoryId = 2 },
            new Product { Id = 5, Name = "Portátil HP OmniBook Ultra Flip 14-fh0005np (14'' - Intel Core Ultra 7 256V - RAM: 16 GB - 1 TB SSD - Intel Arc Graphics)", Description = "Потужний ноутбук. Характеристики цього ноутбуку неможливо описати в двох словах, тому - гугліть, там більше слів:)", Price = 1899.99m, CategoryId = 2 },
            new Product { Id = 6, Name = "Portátil ASUS Vivobook M1502YA (15.6\" - RYZEN 7 7730U - Ram: 16GB - 1TB)", Description = "Потужний ноутбук. Використовується для ігор, праці, та гуглу", Price = 749.99m, CategoryId = 2 }
        );

        // Seed Images
        modelBuilder.Entity<ProductImage>().HasData(
            new ProductImage { Id = 1, ProductId = 1, FileName = "iPhone17_1.jpg" },
            new ProductImage { Id = 2, ProductId = 1, FileName = "iPhone17_2.jpg" },
            new ProductImage { Id = 3, ProductId = 1, FileName = "iPhone17_3.jpg" },
            new ProductImage { Id = 4, ProductId = 2, FileName = "oppo_Reno14_1.jpg" },
            new ProductImage { Id = 5, ProductId = 2, FileName = "oppo_Reno14_2.jpg" },
            new ProductImage { Id = 6, ProductId = 3, FileName = "samsung_phone_1.jpg" },
            new ProductImage { Id = 7, ProductId = 3, FileName = "samsung_phone_2.jpg" },
            new ProductImage { Id = 8, ProductId = 3, FileName = "samsung_phone_3.jpg" },
            new ProductImage { Id = 9, ProductId = 4, FileName = "asus_1.jpg" }, 
            new ProductImage { Id = 10, ProductId = 5, FileName = "hp_1.jpg" },
            new ProductImage { Id = 11, ProductId = 6, FileName = "hp_2.jpg" }
        );

    }
}