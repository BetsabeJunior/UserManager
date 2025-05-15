// <copyright file="User.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Domain.Entities
{
    /// <summary>
    /// Represents a user in the system with personal and identification details.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the identification type.
        /// </summary>
        public int IdentificationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the identification type associated with the user.
        /// </summary>
        public IdentificationType IdentificationType { get; set; }

        /// <summary>
        /// Gets or sets the identification number of the user.
        /// </summary>
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
