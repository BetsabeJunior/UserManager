// <copyright file="IUserService.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserManager.Domain.Entities;

    /// <summary>
    /// Interface for user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of users.</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Get one user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User or null.</returns>
        Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">User to create.</param>
        /// <returns>Task.</returns>
        Task<User> AddAsync(User user);

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="user">User with new data.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(User user);

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(int id);
    }
}
