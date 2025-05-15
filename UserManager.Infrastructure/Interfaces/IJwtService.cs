// <copyright file="IJwtService.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Interfaces
{
    using UserManager.Domain.Entities;

    /// <summary>
    /// Provides functionality to generate JSON Web Tokens (JWT) for users.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <returns>A JWT token as a string.</returns>
        string GenerateToken(User user);
    }
}
