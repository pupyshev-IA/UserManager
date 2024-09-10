using Abdt.Loyal.UserManager.BusinessLogic.Abstractions;
using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.Repository.Abstractions;

namespace Abdt.Loyal.UserManager.BusinessLogic
{
    public class UserService : IAccountManager<User>
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<User> repository, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<User> Register(User item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var (passwordHash, salt) = _passwordHasher.HashPassword(item.PasswordHash);

            var newUser = new User
            {
                Name = item.Name,
                Email = item.Email,
                Salt = salt,
                RegistredAt = item.RegistredAt
            };

            return await _repository.Add(newUser);
        }

        public async Task<string?> Login(string login, string password)
        {
            var user = await _repository.GetByEmail(login);
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var isValidPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt);
            if (!isValidPassword)
                throw new ArgumentException(nameof(login));

            var token = _tokenService.GenerateToken(user);
            return token;
        }

        public async Task<User> Update(User item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            return await _repository.Update(item);
        }

        public Task Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
