// <copyright file="AuthenticationLoginController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UserManager.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;
    using UserManager.Domain.Entities;

    /// <summary>
    /// This class is the Authentication Login controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationLoginController : Controller
    {
        // This is the service to manage users.
        private readonly IUserService _userService;

        public AuthenticationLoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// This method gets the token, fullName and email the users.
        /// </summary>
        /// <returns>List of users.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.AuthenticateAsync(request);

            if (response == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(response);
        }
    }
}
