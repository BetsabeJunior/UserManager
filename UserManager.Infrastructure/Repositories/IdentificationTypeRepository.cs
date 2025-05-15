// <copyright file="IdentificationTypeRepository.cs" company="DITOS SAS">
// Copyright (c) DITOS SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using UserManager.Domain.Entities;
    using UserManager.Infrastructure.Data;
    using UserManager.Infrastructure.Interfaces;

    public class IdentificationTypeRepository : IIdentificationTypeRepository
    {
        private readonly UserManagerDbContext _context;

        public IdentificationTypeRepository(UserManagerDbContext context)
        {
            _context = context;
        }

        public async Task<List<IdentificationType>> GetAllAsync()
        {
            return await _context.Identities.ToListAsync();
        }
    }
}
