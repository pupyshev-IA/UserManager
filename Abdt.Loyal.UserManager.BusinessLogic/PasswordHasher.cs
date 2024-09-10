using Abdt.Loyal.UserManager.BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Abdt.Loyal.UserManager.BusinessLogic
{
    public class PasswordHasher : IPasswordHasher
    {
        public (string hash, string salt) HashPassword(string password)
        {
            var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
                                                Convert.FromBase64String(salt),
                                                KeyDerivationPrf.HMACSHA256,
                                                10000, 32));
            return (hash, salt);
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
                                                Convert.FromBase64String(salt),
                                                KeyDerivationPrf.HMACSHA256,
                                                10000, 32));
            return hashedPassword == hash;
        }
    }
}
