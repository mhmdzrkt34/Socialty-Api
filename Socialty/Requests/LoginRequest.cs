using System.ComponentModel.DataAnnotations;

namespace Socialty.Requests
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@(gmail\.com|outlook\.com|hotmail\.com)$", ErrorMessage = "Email must be from Gmail, Outlook, or Hotmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]+$", ErrorMessage = "Password must contain at least 1 digit, 1 special character, and 1 uppercase letter")]
        public string Password { get; set; }
    }
}
