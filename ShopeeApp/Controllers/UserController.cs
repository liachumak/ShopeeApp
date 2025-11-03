using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopeeApp.Models;
using ShopeeApp.Areas.Admin.Models;

namespace ShopeeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    public async Task<IActionResult> EditRole(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.ToList();

        var model = new EditRoleViewModel
        {
            UserId = user.Id,
            Email = user.Email,
            Roles = allRoles.Select(role => new UserRoleSelection
            {
                RoleName = role.Name,
                IsSelected = userRoles.Contains(role.Name)
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditUserRoleViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
            return NotFound();

        var userRoles = await _userManager.GetRolesAsync(user);
        var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName);

        // видаляємо старі ролі
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        // додаємо вибрані
        await _userManager.AddToRolesAsync(user, selectedRoles);

        TempData["Success"] = "Ролі користувача оновлено!";
        return RedirectToAction(nameof(Index));
    }

    public class EditRoleViewModel
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public List<UserRoleSelection> Roles { get; set; } = new();
}


public class UserRoleSelection
{
    public string RoleName { get; set; }
    public bool IsSelected { get; set; }
}
} 

