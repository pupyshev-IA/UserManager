﻿using Abdt.Loyal.UserManager.Domain;
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
        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /// <inheritdoc />
        public async Task<User?> GetById(long id)
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == item.Id);

            user.Name = item.Name;
            user.PhoneNumber = item.PhoneNumber;

            await _context.SaveChangesAsync();

            return user;
        }

        /// <inheritdoc />
        public async Task Delete(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is not null)
                _context.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
