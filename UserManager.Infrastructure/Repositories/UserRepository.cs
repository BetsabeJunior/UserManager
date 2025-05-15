// <copyright file="UserRepository.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UserManager.Domain.Entities;
    using UserManager.Infrastructure.Interfaces;
    using UserManager.Infrastructure.Data;

    /// <summary>
    /// This class helps to work with users in the database.
    /// You can get, add, update, or delete users.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManagerDbContext _context;

        /// <summary>
        /// Creates the repository with the database connection.
        /// </summary>
        /// <param name="context">The database connection.</param>
        public UserRepository(UserManagerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all users from the database.
        /// </summary>
        /// <returns>List of all users.</returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.IdentificationType)
                .ToListAsync();
        }

        /// <summary>
        /// Get one user by their ID.
        /// </summary>
        /// <param name="id">The user's ID.</param>
        /// <returns>The user with that ID or null.</returns>
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.IdentificationType)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Add a new user to the database.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>The user that was added.</returns>
        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Update an existing user in the database.
        /// </summary>
        /// <param name="user">The user with updated information.</param>
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
