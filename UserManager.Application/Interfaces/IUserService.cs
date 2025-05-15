// <copyright file="IUserService.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserManager.Application.DTOS;
    using UserManager.Domain.Entities;

    /// <summary>
    /// Defines methods for user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all users asynchronously.
        /// </summary>
        /// <returns>A collection of users.</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Gets a user by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user, or null if not found.</returns>
        Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new user asynchronously.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>The created user.</returns>
        Task<User> AddAsync(User user);

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="user">The user with updated information.</param>
        /// <returns>A task representing the operation.</returns>
        Task UpdateAsync(User user);

        /// <summary>
        /// Deletes a user by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A task representing the operation.</returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Authenticates a user based on login credentials.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>An authentication response with the token and user information.</returns>
        Task<AuthResponse> AuthenticateAsync(LoginRequest request);
    }
}
