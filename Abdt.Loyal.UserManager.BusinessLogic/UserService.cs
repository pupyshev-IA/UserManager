using Abdt.Loyal.UserManager.BusinessLogic.Abstractions;
using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.Repository.Abstractions;

namespace Abdt.Loyal.UserManager.BusinessLogic
{
    public class UserService : IUserService<User>
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher _passwordHasher;
        private const string UserErrorMessage = "User with this id does not exist";

        public UserService(IRepository<User> repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<User>> Add(User user)
        {
            var existingUser = await _repository.GetByEmail(user.Email);
            if (existingUser is not null)
                return Result<User>.Failure("User with this email already exists");

            var (passwordHash, salt) = _passwordHasher.HashPassword(user.PasswordHash);

            user.PasswordHash = passwordHash;
            user.Salt = salt;

            var registredUser = await _repository.Add(user);
            return Result<User>.Success(registredUser);
        }

        public async Task<Result<User>> Get(long id)
        {
            var user = await _repository.GetById(id);
            if (user is null)
                return Result<User>.Failure(UserErrorMessage);

            return Result<User>.Success(user);
        }

        public async Task<Result<User>> Update(User user)
        {
            var existingUser = await _repository.GetById(user.Id);
            if (existingUser is null)
                return Result<User>.Failure(UserErrorMessage);

            var updatedUser = await _repository.Update(user);
            return Result<User>.Success(updatedUser);
        }

        public Task Delete(long id)
        {
            return _repository.Delete(id);
        }
    }
}
