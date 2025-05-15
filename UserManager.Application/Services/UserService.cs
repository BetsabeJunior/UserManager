// <copyright file="UserService.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;
    using UserManager.Domain.Entities;
    using UserManager.Infrastructure.Interfaces;

    /// <summary>
    /// Provides services related to user operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="jwtService">The JWT service.</param>
        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
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

        /// <inheritdoc/>
        public async Task<AuthResponse> AuthenticateAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAndPasswordAsync(request.Email, request.Password);

            if (user == null)
            {
                return null;
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
            };
        }
    }
}
