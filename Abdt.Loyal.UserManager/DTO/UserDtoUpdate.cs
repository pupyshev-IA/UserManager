using System.ComponentModel.DataAnnotations;

namespace Abdt.Loyal.UserManager.DTO
{
    public class UserDtoUpdate
    {
        [Required(AllowEmptyStrings = false)]
        [Range(0, long.MaxValue, ErrorMessage = "The value of identifier must be at least zero")]
        public long Id { get; set; }

        [MaxLength(50, ErrorMessage = "Name length more than 50 symbols is not allowed")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email must be specified")]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
