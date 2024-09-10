namespace Abdt.Loyal.UserManager.Domain
{
    public class User
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public DateTimeOffset RegistredAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
