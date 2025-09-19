using Microsoft.AspNetCore.Mvc;

namespace ShopeeApp.Controllers;

public class CategoriesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

