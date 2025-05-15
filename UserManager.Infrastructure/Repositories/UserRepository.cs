// <copyright file="UserRepository.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Repositories
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UserManager.Domain.Entities;
    using UserManager.Domain.Interfaces;
    using UserManager.Infrastructure.Data;

    /// <summary>
    /// Class to work with users in database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManagerDbContext _context;

        /// <summary>
        /// Constructor with database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        public UserRepository(UserManagerDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.IdentificationType)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
