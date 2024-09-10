namespace Abdt.Loyal.UserManager.DTO
{
    public class UserDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTimeOffset RegistredAt { get; set; }
    }
}
