// <copyright file="IdentificationType.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Domain.Entities
{
    /// <summary>
    /// Represents a type of identification document (e.g., CC, TI, PA).
    /// </summary>
    public class IdentificationType
    {
        /// <summary>
        /// Gets or sets the unique identifier for the identification type.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the code of the identification type (e.g., "CC", "TI").
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the full name of the identification type.
        /// </summary>
        public string Name { get; set; }
    }
}
