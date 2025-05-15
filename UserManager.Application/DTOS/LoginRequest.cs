// <copyright file="LoginRequest.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.DTOS
{
    /// <summary>
    /// Represents the login request data.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password { get; set; }
    }
}
