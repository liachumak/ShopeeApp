using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopeeApp.Data;
using ShopeeApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Додати DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// 2. Додати Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3. Налаштування cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

// 4. Додати MVC підтримку
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 5. Мідлвари
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// авторизація користувачів
app.UseAuthentication(); 
app.UseAuthorization();

// 6. Маршрутизація
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
