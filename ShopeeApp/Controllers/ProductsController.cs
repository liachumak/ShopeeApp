using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopeeApp.Data;
using ShopeeApp.Models.Product;
using ShopeeApp.ViewModels;

namespace ShopeeApp.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View(model);
        }

        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            CategoryId = model.CategoryId,
            Images = new List<ProductImage>()
        };

        // Збереження фото
        if (model.Images != null && model.Images.Any())
        {
            string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            int priority = 1;
            foreach (var file in model.Images)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(((IFormFile)file).FileName);
                var filePath = Path.Combine(imagesPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ((IFormFile)file).CopyToAsync(stream);
                }

                product.Images.Add(new ProductImage
                {
                    FileName = fileName,
                    Priority = priority++
                });
            }
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();

        var model = new ProductEditViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            ExistingImages = product.Images.Select(i => new ProductImageViewModel
            {
                Id = i.Id,
                FileName = i.FileName,
                Priority = i.Priority
            }).ToList()
        };

        ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", model.CategoryId);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductEditViewModel model, [FromForm] List<string> ImageOrder, [FromForm] List<int> DeletedImageIds)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == model.Id);

        if (product == null) return NotFound();

        // Оновлення полів
        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;
        product.CategoryId = model.CategoryId;

        // Видалення фото
        if (DeletedImageIds != null && DeletedImageIds.Any())
        {
            var toDelete = product.Images.Where(i => DeletedImageIds.Contains(i.Id)).ToList();
            foreach (var img in toDelete)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", img.FileName);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                product.Images.Remove(img);
            }
        }

        // Додавання нових фото
        if (model.NewImages != null && model.NewImages.Any())
        {
            string imageDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(imageDir)) Directory.CreateDirectory(imageDir);

            foreach (var file in model.NewImages)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(((IFormFile)file).FileName);
                var filePath = Path.Combine(imageDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ((IFormFile)file).CopyToAsync(stream);
                }

                product.Images.Add(new ProductImage
                {
                    FileName = fileName,
                    Priority = 0, // тимчасовий пріоритет
                    ProductId = product.Id
                });
            }
        }

        // Оновлення порядку всіх зображень
        if (ImageOrder != null && ImageOrder.Any())
        {
            int priority = 1;
            foreach (var item in ImageOrder)
            {
                var parts = item.Split(':');
                if (parts.Length != 2) continue;

                var type = parts[0];
                var id = parts[1];

                if (type == "existing")
                {
                    if (int.TryParse(id, out int existingId))
                    {
                        var img = product.Images.FirstOrDefault(i => i.Id == existingId);
                        if (img != null)
                            img.Priority = priority++;
                    }
                }
                else if (type == "new")
                {
                    var img = product.Images.FirstOrDefault(i => i.Priority == 0);
                    if (img != null)
                        img.Priority = priority++;
                }
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

 
}
