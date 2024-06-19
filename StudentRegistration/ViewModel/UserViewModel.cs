using System.ComponentModel.DataAnnotations;

namespace StudentRegistration.ViewModel
{
    public class UserViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public string Role { get; set; }
    }
}
