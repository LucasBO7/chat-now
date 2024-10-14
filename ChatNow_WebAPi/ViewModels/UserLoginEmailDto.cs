using System.ComponentModel.DataAnnotations;

namespace ChatNow_WebAPi.ViewModels
{
    public class UserLoginEmailDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
