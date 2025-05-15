// <copyright file="UsersControllerTests.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using UserManager.API.Common.Responses;
    using UserManager.API.Controllers;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;
    using UserManager.Domain.Entities;

    /// <summary>
    /// This class tests the UsersControllerTests.
    /// </summary>
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> mockUserService;
        private readonly Mock<ILogger<UsersController>> mockLogger;
        private readonly UsersController controller;

        /// <summary>
        /// Initializes the test class.
        /// </summary>
        public UsersControllerTests()
        {
            this.mockUserService = new Mock<IUserService>();
            this.mockLogger = new Mock<ILogger<UsersController>>();
            this.controller = new UsersController(this.mockUserService.Object, this.mockLogger.Object);
        }

        /// <summary>
        /// Test for the GetAll method to check if it returns all users.
        /// </summary>
        [Fact]
        public async Task GetAll_ReturnsAllUsers()
        {
            // Arrange
            var identificationType = new IdentificationType { Id = 1, Code = "CC", Name = "Cédula de Ciudadanía" };

            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Juan",
                    LastName = "Pérez",
                    IdentificationTypeId = identificationType.Id,
                    IdentificationType = identificationType,
                    IdentificationNumber = "7777777",
                    Email = "juan@gmail.com",
                    Password = "123456",
                },
            };

            this.mockUserService
                .Setup(service => service.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = await this.controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<IEnumerable<User>>>(okResult.Value);
            Assert.True(response.Success);
            Assert.Single(response.Data);
            var user = Assert.Single(response.Data);
            Assert.Equal("Juan", user.FirstName);
            Assert.Equal("Pérez", user.LastName);
            Assert.Equal("juan@gmail.com", user.Email);
        }

        /// <summary>
        /// Test for GetById to return BadRequest when id is invalid.
        /// </summary>
        [Fact]
        public async Task GetById_InvalidId_ReturnsBadRequest()
        {
            // Act
            var result = await this.controller.GetById(0);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(badRequest.Value);
            Assert.False(response.Success);
        }

        /// <summary>
        /// Test for GetById to return NotFound when user is not found.
        /// </summary>
        [Fact]
        public async Task GetById_UserNotFound_ReturnsNotFound()
        {
            mockUserService
                .Setup(service => service.GetByIdAsync(99))
                .ReturnsAsync((User?)null);

            // Act
            var result = await this.controller.GetById(99);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(notFound.Value);
            Assert.False(response.Success);
        }

        /// <summary>
        /// Test for GetById to return user data when user is found.
        /// </summary>
        [Fact]
        public async Task GetById_UserFound_ReturnsOk()
        {
            var user = new User { Id = 1, FirstName = "BetsabeHoyos", Email = "pruebasBetsa@gmail.com" };

            this.mockUserService
                .Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(user);

            // Act
            var result = await this.controller.GetById(1);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<User>>(ok.Value);
            Assert.True(response.Success);
            Assert.Equal(1, response.Data.Id);
        }

        /// <summary>
        /// Test for Create to return BadRequest when email is invalid.
        /// </summary>
        [Fact]
        public async Task Create_InvalidEmail_ReturnsBadRequest()
        {
            var dto = new CreateUserRequest { Email = "pruebasBetsa", Password = "123456" };

            var result = await this.controller.Create(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(badRequest.Value);
            Assert.False(response.Success);
        }

        /// <summary>
        /// Test for Create to return BadRequest when password is too short.
        /// </summary>
        [Fact]
        public async Task Create_ShortPassword_ReturnsBadRequest()
        {
            var dto = new CreateUserRequest { Email = "pruebasBetsa@gmail.com", Password = "123" };

            var result = await this.controller.Create(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(badRequest.Value);
            Assert.False(response.Success);
        }

        /// <summary>
        /// Test for Create to successfully create a user.
        /// </summary>
        [Fact]
        public async Task Create_ValidUser_ReturnsCreatedUser()
        {
            var dto = new CreateUserRequest
            {
                Email = "pruebasBetsa@gmail.com",
                Password = "123456",
                FirstName = "Betsabe",
                LastName = "Hoyos",
                IdentificationNumber = "123",
                IdentificationTypeId = 1
            };

            var user = new User { Id = 1, Email = dto.Email };

            this.mockUserService
                .Setup(s => s.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(user);

            var result = await this.controller.Create(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var response = Assert.IsType<ApiResponse<User>>(created.Value);
            Assert.True(response.Success);
            Assert.Equal("pruebasBetsa@gmail.com", response.Data.Email);
        }

        /// <summary>
        /// Test for Update to return BadRequest when id is invalid.
        /// </summary>
        [Fact]
        public async Task Update_InvalidId_ReturnsBadRequest()
        {
            var dto = new UpdateUserRequest { Email = "pruebasBetsa@gmail.com", Password = "123456" };

            var result = await this.controller.Update(0, dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(badRequest.Value);
            Assert.False(response.Success);
        }

        /// <summary>
        /// Test for Update to return NotFound when user is not found.
        /// </summary>
        [Fact]
        public async Task Update_UserNotFound_ReturnsNotFound()
        {
            var dto = new UpdateUserRequest { Email = "pruebasBetsa@gmail.com", Password = "123456" };

            this.mockUserService
                .Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync((User?)null);

            var result = await this.controller.Update(1, dto);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(notFound.Value);
            Assert.False(response.Success);
        }

        /// <summary>
        /// Test for Update to successfully update a user.
        /// </summary>
        [Fact]
        public async Task Update_ValidUser_ReturnsOk()
        {
            var dto = new UpdateUserRequest { Email = "pruebasBetsa@gmail.com", Password = "123456" };
            var user = new User { Id = 1, Email = "pruebasBetsa2@gmail.com" };

            this.mockUserService
                .Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(user);

            this.mockUserService
                .Setup(service => service.UpdateAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var result = await this.controller.Update(1, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<User>>(ok.Value);
            Assert.True(response.Success);
            Assert.Equal("pruebasBetsa@gmail.com", response.Data.Email);
        }

        /// <summary>
        /// Test for Delete to return BadRequest when id is invalid.
        /// </summary>
        [Fact]
        public async Task Delete_InvalidId_ReturnsBadRequest()
        {
            var result = await this.controller.Delete(0);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(badRequest.Value);
            Assert.False(response.Success);
        }

        /// <summary>
        /// Test for Delete to return NotFound when user is not found.
        /// </summary>
        [Fact]
        public async Task Delete_UserNotFound_ReturnsNotFound()
        {
            this.mockUserService
                .Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync((User?)null);

            var result = await this.controller.Delete(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(notFound.Value);
            Assert.False(response.Success);
        }

        /// <summary>
        /// Test for Delete to successfully delete a user.
        /// </summary>
        [Fact]
        public async Task Delete_ValidUser_ReturnsOk()
        {
            var user = new User { Id = 1, Email = "pruebasBetsa@gmail.com" };

            this.mockUserService
                .Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(user);

            this.mockUserService
                .Setup(service => service.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            var result = await this.controller.Delete(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(ok.Value);
            Assert.True(response.Success);
            Assert.Equal("User deleted.", response.Data);
        }
    }
}
