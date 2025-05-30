// <copyright file="UsersController.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.ComponentModel.DataAnnotations;
    using UserManager.API.Common.Responses;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;
    using UserManager.Domain.Entities;
    using UserManager.Infrastructure.Interfaces;

    /// <summary>
    /// This controller helps to manage users (create, read, update, delete).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IIdentificationTypeRepository _repository;

        /// <summary>
        /// Constructor. Gets services to use in this controller.
        /// </summary>
        /// <param name="userService">Service for user actions.</param>
        /// <param name="logger">Logger to write information in console or file.</param>
        public UsersController(IUserService userService, ILogger<UsersController> logger, IIdentificationTypeRepository identificationTypeRepository)
        {
            _userService = userService;
            _logger = logger;
            _repository = identificationTypeRepository;
        }

        /// <summary>
        /// Get all users in the system.
        /// </summary>
        /// <returns>List of all users.</returns>
        [Authorize]
        [HttpGet("GetAllUser")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDTOs>>), 200)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all users");
            var users = await _userService.GetAllAsync();
            var userDtos = new List<UserDTOs>();

            foreach (var user in users)
            {
                userDtos.Add(new UserDTOs
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IdentificationNumber = user.IdentificationNumber,
                    Code = user.IdentificationType.Code,
                    Name = user.IdentificationType?.Name,
                    Email = user.Email,
                    Password = user.Password,
                });
            }

            return Ok(ApiResponse<IEnumerable<UserDTOs>>.Ok(userDtos));
        }

        /// <summary>
        /// Get one user using ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>User with that ID.</returns>
        [Authorize]
        [HttpGet("GetUserId{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserDTOs>), 200)]
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

            var userDtos = new UserDTOs
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IdentificationNumber = user.IdentificationNumber,
                Code = user.IdentificationType.Code,
                Name = user.IdentificationType.Name,
                Email = user.Email,
                Password = user.Password,
            };

            return Ok(ApiResponse<UserDTOs>.Ok(userDtos));
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="dto">User data to save.</param>
        /// <returns>The created user.</returns>
        [Authorize]
        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(ApiResponse<CreateUserRequest>), 201)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest dto)
        {
            var identificationType = await _repository.GetByIdAsync(dto.IdentificationTypeId);
            if (identificationType == null)
            {
                return BadRequest(ApiResponse<string>.Fail("Identification type is not valid."));
            }

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
                Password = dto.Password,
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
        [ProducesResponseType(typeof(ApiResponse<UpdateUserRequest>), 200)]
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

            var emailValidator = new EmailAddressAttribute();
            if (!string.IsNullOrWhiteSpace(dto.Email) && !new EmailAddressAttribute().IsValid(dto.Email))
            {
                return BadRequest(ApiResponse<string>.Fail("Email is not valid."));
            }

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                if (dto.Password.Length < 6)
                {
                    return BadRequest(ApiResponse<string>.Fail("Password must be at least 6 characters."));
                }

                user.Password = dto.Password;
            }

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                user.FirstName = dto.FirstName;

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                user.LastName = dto.LastName;

            if (dto.IdentificationTypeId > 0)
                user.IdentificationTypeId = dto.IdentificationTypeId;

            if (!string.IsNullOrWhiteSpace(dto.IdentificationNumber))
                user.IdentificationNumber = dto.IdentificationNumber;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email;

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
