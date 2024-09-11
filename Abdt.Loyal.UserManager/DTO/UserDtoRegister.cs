using System.ComponentModel.DataAnnotations;

namespace Abdt.Loyal.UserManager.DTO
{
    public class UserDtoRegister
    {
        [MaxLength(50, ErrorMessage = "Name length more than 50 symbols is not allowed")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email must be specified")]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password must be specified")]
        public string PasswordHash { get; set; }
    }
}
