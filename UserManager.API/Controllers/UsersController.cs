// <copyright file="UsersController.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;
    using UserManager.Application.Services;
    using UserManager.Domain.Entities;

    /// <summary>
    /// This is controller the user.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(int id)
        {
            return await _userService.GetByIdAsync(id);
        }

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
    }
}
