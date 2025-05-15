// <copyright file="UserService.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserManager.Application.Interfaces;
    using UserManager.Domain.Entities;
    using UserManager.Domain.Interfaces;

    /// <summary>
    /// This class manages users.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor with user repository.
        /// </summary>
        /// <param name="userRepository">User repository.</param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        /// <inheritdoc/>
        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public async Task<User> AddAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
