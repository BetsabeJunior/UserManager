// <copyright file="DomainException.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>
namespace UserManager.Domain.Exceptions
{
    /// <summary>
    /// This class is for errors in the domain.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// Create a new DomainException without message.
        /// </summary>
        public DomainException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// Create a new DomainException with a message.
        /// </summary>
        /// <param name="message">Error message.</param>
        public DomainException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException"/> class.
        /// Create a new DomainException with a message and inner error.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner error.</param>
        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
