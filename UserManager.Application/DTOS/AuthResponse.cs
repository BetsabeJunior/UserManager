// <copyright file="AuthResponse.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.DTOS
{
    /// <summary>
    /// Represents the response returned after successful authentication.
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Gets or sets the JWT token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the authenticated user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the authenticated user's full name.
        /// </summary>
        public string FullName { get; set; }
    }
}
