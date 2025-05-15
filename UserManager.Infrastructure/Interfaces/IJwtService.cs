// <copyright file="IJwtService.cs" company="Dito SAS">
// Copyright (c) Dito SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Interfaces
{
    using UserManager.Domain.Entities;

    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
