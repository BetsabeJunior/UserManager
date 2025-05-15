// <copyright file="CreateUserRequest.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.DTOS
{
    /// <summary>
    /// This class is used to create a new user.
    /// It includes the user first name, last name,
    /// ID type and number, password, and email.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// The user first name.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// The user last name.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// The ID type number (for example: 1 for Cédula de Ciudadanía, 2 for Tarjeta de Identidad).
        /// </summary>
        public int IdentificationTypeId { get; set; }

        /// <summary>
        /// The user ID number.
        /// </summary>
        public string IdentificationNumber { get; set; } = null!;

        /// <summary>
        /// The user password.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// The user email address.
        /// </summary>
        public string Email { get; set; } = null!;
    }
}
