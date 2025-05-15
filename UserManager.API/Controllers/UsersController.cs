// <copyright file="UsersController.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;
    using UserManager.Domain.Entities;

    /// <summary>
    /// This class is the user controller.
    /// It helps to create, read, update and delete users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // This is the service to manage users.
        private readonly IUserService _userService;

        /// <summary>
        /// This is the constructor.
        /// It receives the user service.
        /// </summary>
        /// <param name="userService">This is the user service.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// This method gets all users.
        /// </summary>
        /// <returns>List of users.</returns>
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userService.GetAllAsync();
        }

        /// <summary>
        /// This method gets one user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>The user with the given ID.</returns>
        [HttpGet("{id}")]
        public async Task<User?> GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await _userService.GetByIdAsync(id);
        }

        /// <summary>
        /// This method creates a new user.
        /// </summary>
        /// <param name="dto">This is the new user data.</param>
        /// <returns>A response with the new user.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IdentificationTypeId = dto.IdentificationTypeId,
                IdentificationNumber = dto.IdentificationNumber,
                Email = dto.Email,
                Password = dto.Password
            };

            var createdUser = await _userService.AddAsync(user);

            return CreatedAtAction(nameof(GetAll), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// This method updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="dto">The new data for the user.</param>
        /// <returns>A response with status OK or error.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest dto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IdentificationTypeId = dto.IdentificationTypeId;
            user.IdentificationNumber = dto.IdentificationNumber;
            user.Email = dto.Email;
            user.Password = dto.Password;
            await _userService.UpdateAsync(user);
            return Ok(user);
        }

        /// <summary>
        /// This method deletes a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A response with status OK or error.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}
