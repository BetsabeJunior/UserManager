// <copyright file="IIdentificationTypeRepository.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserManager.Domain.Entities;

    /// <summary>
    /// Provides access to identification types from the data source.
    /// </summary>
    public interface IIdentificationTypeRepository
    {
        /// <summary>
        /// Gets all identification types asynchronously.
        /// </summary>
        /// <returns>A list of identification types.</returns>
        Task<List<IdentificationType>> GetAllAsync();

        /// <summary>
        /// Gets identification by id.
        /// </summary>
        /// <returns>A identification types.</returns>
        Task<IdentificationType> GetByIdAsync(int id);
    }
}