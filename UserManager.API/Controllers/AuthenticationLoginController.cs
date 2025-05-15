// <copyright file="AuthenticationLoginController.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;

    /// <summary>
    /// This controller is for login. People use this to enter to system.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationLoginController : Controller
    {
        // This is the service for user things (login, etc).
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for this controller. Put user service here.
        /// </summary>
        /// <param name="userService">Service to work with users.</param>
        public AuthenticationLoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// This method is for login. You give email and password. It give token if correct.
        /// </summary>
        /// <param name="request">This is the login request. It has email and password.</param>
        /// <returns>If ok, return token and info. If bad, return unauthorized.</returns>
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
