using Abdt.Loyal.UserManager.BusinessLogic.Abstractions;
using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.Repository.Abstractions;
using Microsoft.Extensions.Logging;

namespace Abdt.Loyal.UserManager.BusinessLogic
{
    public class UserService : IUserService<User>
    {
        private readonly ILogger<UserService> _logger;
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher _passwordHasher;
        private const string UserErrorMessage = "User with this id does not exist";

        public UserService(ILogger<UserService> logger, IRepository<User> repository, IPasswordHasher passwordHasher)
        {
            _logger = logger;
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<User>> Add(User user)
        {
            using var scope = _logger.BeginScope("");

            var existingUser = await _repository.GetByEmail(user.Email);
            if (existingUser is not null)
            {
                _logger.LogError("Unable to add a user with email=\"{email}\"", user.Email);
                return Result<User>.Failure("User with this email already exists");
            }

            var (passwordHash, salt) = _passwordHasher.HashPassword(user.PasswordHash);
            user.PasswordHash = passwordHash;
            user.Salt = salt;

            var registredUser = await _repository.Add(user);

            _logger.LogInformation("Added a user with id=\"{id}\" name=\"{name}\", email=\"{email}\"", user.Id, user.Name, user.Email);
            return Result<User>.Success(registredUser);
        }

        public async Task<Result<User>> Get(long id)
        {
            var user = await _repository.GetById(id);
            if (user is null)
            {
                _logger.LogError("Unable to get a user with id=\"{id}\"", id);
                return Result<User>.Failure(UserErrorMessage);
            }

            _logger.LogInformation("Getting specified user with id=\"{id}\"", id);
            return Result<User>.Success(user);
        }

        public async Task<Result<User>> Update(User user)
        {
            var existingUser = await _repository.GetById(user.Id);
            if (existingUser is null)
            {
                _logger.LogError("Unable to update a user");
                return Result<User>.Failure(UserErrorMessage);
            }

            var updatedUser = await _repository.Update(user);
            _logger.LogInformation("Updating a user: new name=\"{name}\", new phoneNum=\"{phoneNum}\"", user.Name, user.PhoneNumber);

            return Result<User>.Success(updatedUser);
        }

        public Task Delete(long id)
        {
            _logger.LogInformation("Deletion a user with id=\"{id}\"", id);
            return _repository.Delete(id);
        }
    }
}
