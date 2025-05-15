// <copyright file="IdentificationTypeRepository.cs" company="DITOS SAS">
// Copyright (c) DITOS SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UserManager.Domain.Entities;
    using UserManager.Infrastructure.Data;
    using UserManager.Infrastructure.Interfaces;

    /// <summary>
    /// Repository for getting identification types from the database.
    /// </summary>
    public class IdentificationTypeRepository : IIdentificationTypeRepository
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly UserManagerDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentificationTypeRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public IdentificationTypeRepository(UserManagerDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<List<IdentificationType>> GetAllAsync()
        {
            return await _context.Identities.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IdentificationType> GetByIdAsync(int id)
        {
            return await _context.Identities.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}