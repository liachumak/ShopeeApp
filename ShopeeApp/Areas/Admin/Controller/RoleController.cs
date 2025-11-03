using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopeeApp.Models;

namespace ShopeeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // список з ролями 
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var model = new List<RoleWithUsersViewModel>();

            foreach (var role in roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(role.Name);
                model.Add(new RoleWithUsersViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Users = users.Select(u => u.FullName ?? u.UserName ?? u.Email).ToList()
                });
            }

            return View(model);
        }

        // перегляд ролі бзера
        public async Task<IActionResult> Users(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            var model = new RoleUsersViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Users = users.ToList()
            };

            return View(model);
        }

        // видалення юзера з ролі
        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            if (await _userManager.IsInRoleAsync(user, roleName))
                await _userManager.RemoveFromRoleAsync(user, roleName);

            TempData["Success"] = $"Користувач {user.FullName ?? user.Email} видалений з ролі {roleName}";
            return RedirectToAction("Users", new { id = (await _roleManager.FindByNameAsync(roleName)).Id });
        }

        // створення ролі
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ModelState.AddModelError("", "Назва ролі не може бути порожньою");
                return View();
            }

            var exists = await _roleManager.RoleExistsAsync(name);
            if (exists)
            {
                ModelState.AddModelError("", "Така роль уже існує");
                return View();
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(name.Trim()));
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }

        //видалення ролей
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users.Any())
            {
                TempData["Error"] = "Не можна видалити роль, поки до неї прив’язані користувачі.";
                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            await _roleManager.DeleteAsync(role);
            TempData["Success"] = $"Роль '{role.Name}' успішно видалена.";
            return RedirectToAction(nameof(Index));
        }
    }

    // Моделі
    public class RoleWithUsersViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> Users { get; set; } = new();
    }

    public class RoleUsersViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ApplicationUser> Users { get; set; } = new();
    }
}
