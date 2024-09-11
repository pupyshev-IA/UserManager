using System.ComponentModel.DataAnnotations;

namespace Abdt.Loyal.UserManager.DTO
{
    public class UserDtoLogin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Login must be specified")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password must be specified")]
        public string PasswordHash { get; set; }
    }
}
