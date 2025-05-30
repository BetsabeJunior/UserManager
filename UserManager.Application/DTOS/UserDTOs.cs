// <copyright file="UserDTOs.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.DTOS
{
    public class UserDTOs
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user first name.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// The user last name.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the code of the identification type (e.g., "CC", "TI").
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the full name of the identification type.
        /// </summary>
        public string Name { get; set; }

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
