using Abdt.Loyal.UserManager.BusinessLogic.Abstractions;
using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.Repository.Abstractions;

namespace Abdt.Loyal.UserManager.BusinessLogic
{
    public class UserService : IAccountManager<User>
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher _passwordHasher;
        private const string AuthErrorMessage = "Invalid login or password";

        public UserService(IRepository<User> repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<User>> Register(User user)
        {
            var (passwordHash, salt) = _passwordHasher.HashPassword(user.PasswordHash);

            user.PasswordHash = passwordHash;
            user.Salt = salt;

            var registredUser = await _repository.Add(user);
            return Result<User>.Success(registredUser);
        }

        public async Task<Result<User>> Login(string login, string password)
        {
            var user = await _repository.GetByEmail(login);
            if (user is null)
                return Result<User>.Failure(AuthErrorMessage);

            var isValidPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt);
            if (!isValidPassword)
                return Result<User>.Failure(AuthErrorMessage);

            return Result<User>.Success(user);
        }

        public async Task<Result<User>> Update(User user)
        {
            var updatedUser = await _repository.Update(user);
            return Result<User>.Success(updatedUser);
        }

        public Task Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
