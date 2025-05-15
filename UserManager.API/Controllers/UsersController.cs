// <copyright file="UsersController.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.ComponentModel.DataAnnotations;
    using UserManager.API.Common.Responses;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;
    using UserManager.Domain.Entities;

    /// <summary>
    /// This controller helps to manage users (create, read, update, delete).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        /// <summary>
        /// Constructor. Gets services to use in this controller.
        /// </summary>
        /// <param name="userService">Service for user actions.</param>
        /// <param name="logger">Logger to write information in console or file.</param>
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Get all users in the system.
        /// </summary>
        /// <returns>List of all users.</returns>
        [Authorize]
        [HttpGet("GetAllUser")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<User>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all users");
            var users = await _userService.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<User>>.Ok(users));
        }

        /// <summary>
        /// Get one user using ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>User with that ID.</returns>
        [Authorize]
        [HttpGet("GetUserId{id}")]
        [ProducesResponseType(typeof(ApiResponse<User>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid ID: {Id}", id);
                return BadRequest(ApiResponse<string>.Fail("ID must be greater than zero."));
            }

            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User not found with ID: {Id}", id);
                return NotFound(ApiResponse<string>.Fail("User not found."));
            }

            return Ok(ApiResponse<User>.Ok(user));
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="dto">User data to save.</param>
        /// <returns>The created user.</returns>
        [Authorize]
        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(ApiResponse<User>), 201)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest dto)
        {
            var emailValidator = new EmailAddressAttribute();
            if (string.IsNullOrWhiteSpace(dto.Email) || !emailValidator.IsValid(dto.Email))
            {
                return BadRequest(ApiResponse<string>.Fail("Email is not valid."));
            }

            if (dto.Password.Length < 6)
            {
                return BadRequest(ApiResponse<string>.Fail("Password must be at least 6 characters."));
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IdentificationTypeId = dto.IdentificationTypeId,
                IdentificationNumber = dto.IdentificationNumber,
                Email = dto.Email,
                Password = dto.Password
            };

            _logger.LogInformation("Creating user with email: {Email}", dto.Email);
            var createdUser = await _userService.AddAsync(user);
            var response = ApiResponse<User>.Ok(createdUser, "User created.");
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, response);
        }

        /// <summary>
        /// Update a user using ID.
        /// </summary>
        /// <param name="id">User ID to update.</param>
        /// <param name="dto">New data to update.</param>
        /// <returns>The updated user or error.</returns>
        [Authorize]
        [HttpPut("UpdateUser{id}")]
        [ProducesResponseType(typeof(ApiResponse<User>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest dto)
        {
            if (id <= 0)
            {
                return BadRequest(ApiResponse<string>.Fail("Invalid ID."));
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(ApiResponse<string>.Fail("User not found."));
            }

            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
            {
                return BadRequest(ApiResponse<string>.Fail("Email is not valid."));
            }

            if (dto.Password.Length < 6)
            {
                return BadRequest(ApiResponse<string>.Fail("Password must be at least 6 characters."));
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IdentificationTypeId = dto.IdentificationTypeId;
            user.IdentificationNumber = dto.IdentificationNumber;
            user.Email = dto.Email;
            user.Password = dto.Password;

            await _userService.UpdateAsync(user);
            return Ok(ApiResponse<User>.Ok(user, "User updated."));
        }

        /// <summary>
        /// Delete a user using ID.
        /// </summary>
        /// <param name="id">User ID to delete.</param>
        /// <returns>Status of the delete.</returns>
        [Authorize]
        [HttpDelete("DeleteUser{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ApiResponse<string>.Fail("Invalid ID."));
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(ApiResponse<string>.Fail("User not found."));
            }

            await _userService.DeleteAsync(id);
            return Ok(ApiResponse<string>.Ok("User deleted."));
        }
    }
}
