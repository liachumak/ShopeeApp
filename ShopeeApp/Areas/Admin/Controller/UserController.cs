using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopeeApp.Areas.Admin.Models;
using ShopeeApp.Models;

namespace ShopeeApp.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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
            return NotFound();

        var roles = _roleManager.Roles.ToList();
        var userRoles = await _userManager.GetRolesAsync(user);

        var model = new EditUserRoleViewModel
        {
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            Roles = roles.Select(r => new RoleSelection
            {
                RoleName = r.Name ?? string.Empty,           
                IsSelected = userRoles.Contains(r.Name)     
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(EditUserRoleViewModel model)
    {
        if (!ModelState.IsValid)
        {
           
            var allRoles = _roleManager.Roles.ToList();
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userRoleNames = user != null ? await _userManager.GetRolesAsync(user) : new List<string>();

            model.Roles = allRoles.Select(r => new RoleSelection
            {
                RoleName = r.Name ?? string.Empty,
                IsSelected = userRoleNames.Contains(r.Name)
            }).ToList();

            return View(model);
        }

        var userToUpdate = await _userManager.FindByIdAsync(model.UserId);
        if (userToUpdate == null)
            return NotFound();

        var currentRoles = await _userManager.GetRolesAsync(userToUpdate);
        var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName).ToList();

        var rolesToAdd = selectedRoles.Except(currentRoles);
        var rolesToRemove = currentRoles.Except(selectedRoles);

        if (rolesToAdd.Any())
        {
            var addResult = await _userManager.AddToRolesAsync(userToUpdate, rolesToAdd);
            if (!addResult.Succeeded)
            {
                foreach (var err in addResult.Errors) ModelState.AddModelError("", err.Description);
                
                return View(model);
            }
        }

        if (rolesToRemove.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(userToUpdate, rolesToRemove);
            if (!removeResult.Succeeded)
            {
                foreach (var err in removeResult.Errors) ModelState.AddModelError("", err.Description);
              
                return View(model);
            }
        }

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        // Не можна видалити адміністратора
        if (await _userManager.IsInRoleAsync(user, "Admin"))
        {
            TempData["Error"] = "Неможливо видалити адміністратора.";
            return RedirectToAction(nameof(Index));
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            TempData["Success"] = $"Користувача {user.Email} видалено.";
        }
        else
        {
            TempData["Error"] = "Помилка при видаленні користувача.";
        }

        return RedirectToAction(nameof(Index));
    }


    
}

