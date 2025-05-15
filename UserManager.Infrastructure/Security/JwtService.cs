// <copyright file="JwtService.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Infrastructure.Security
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using UserManager.Domain.Entities;
    using UserManager.Infrastructure.Interfaces;

    /// <summary>
    /// This class help make token (JWT) for user login.
    /// </summary>
    public class JwtService : IJwtService
    {
        /// <summary>
        /// This is config. It has secret key and info for JWT.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// This is constructor. It puts config inside the class.
        /// </summary>
        /// <param name="configuration">This is the config from app settings.</param>
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// This method make a JWT token for one user.
        /// </summary>
        /// <param name="user">This is the user to make token.</param>
        /// <returns>This give back a token in string.</returns>
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
