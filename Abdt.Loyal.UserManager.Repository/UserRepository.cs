using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Abdt.Loyal.UserManager.Repository
{
    public class UserRepository : IRepository<User>
    {
        private UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<User> Add(User item)
        {
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        /// <inheritdoc />
        public async Task<User?> GetById(Guid id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        /// <inheritdoc />
        public async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        /// <inheritdoc />
        public async Task<User> Update(User item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            _context.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        /// <inheritdoc />
        public async Task Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is not null)
                _context.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
