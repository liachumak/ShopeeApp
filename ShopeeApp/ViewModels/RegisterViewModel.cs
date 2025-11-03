using System.ComponentModel.DataAnnotations;

namespace ShopeeApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введіть ім’я")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Введіть Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Підтвердіть пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються")]
        public string ConfirmPassword { get; set; }

        // Поле для завантаження зображення
        public IFormFile? ProfileImage { get; set; }
    }
}
