namespace Abdt.Loyal.UserManager.Domain
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public DateTimeOffset RegistredAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
