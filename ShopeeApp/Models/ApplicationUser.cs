using Microsoft.AspNetCore.Identity;

namespace ShopeeApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
