// <copyright file="IIdentificationTypeRepository.cs" company="DITOS SAS">
// Copyright (c) DITOS SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Interfaces
{
    using UserManager.Domain.Entities;

    public interface IIdentificationTypeRepository
    {
        Task<List<IdentificationType>> GetAllAsync();
    }
}
