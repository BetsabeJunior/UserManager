// <copyright file="AuthenticationLoginControllerTests.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Tests.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using UserManager.API.Controllers;
    using UserManager.Application.DTOS;
    using UserManager.Application.Interfaces;
    using Xunit;

    /// <summary>
    /// This class test the AuthenticationLoginController. It checks if login works.
    /// </summary>
    public class AuthenticationLoginControllerTests
    {
        private readonly Mock<IUserService> mockUserService;
        private readonly AuthenticationLoginController controller;

        /// <summary>
        /// Constructor. It make the controller with mock service.
        /// </summary>
        public AuthenticationLoginControllerTests()
        {
            this.mockUserService = new Mock<IUserService>();
            this.controller = new AuthenticationLoginController(this.mockUserService.Object);
        }

        /// <summary>
        /// This test is for correct login. It return OK with token.
        /// </summary>
        /// <returns>HTTP 200 with login response.</returns>
        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "prueba@gmail.com",
                Password = "password123"
            };

            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJiZXRzYWJlaG95b3NAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkJldHNhYmUgSnVuaW9yIEhveW9zIEJhcnJpb3MiLCJleHAiOjE3NDczMjQ4NzQsImlzcyI6IlVzZXJNYW5hZ2VyQVBJIiwiYXVkIjoiVXNlck1hbmFnZXJDbGllbnQifQ.6uElK5tOolMg2XFliQYl4YL0XEOAiqm9_A5OK5wPUyo";

            var expectedResponse = new AuthResponse
            {
                Token = token,
                Email = "prueba@gmail.com",
                FullName = "Prueba Betsabe"
            };

            mockUserService
                .Setup(service => service.AuthenticateAsync(It.Is<LoginRequest>(r =>
                    r.Email == request.Email && r.Password == request.Password)))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<AuthResponse>(okResult.Value);
            Assert.Equal(token, response.Token);
        }

        /// <summary>
        /// This test is for wrong login. It return Unauthorized.
        /// </summary>
        /// <returns>HTTP 401 unauthorized.</returns>
        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "pruebasBetsa@gmail.com",
                Password = "789654123"
            };

            mockUserService
                .Setup(service => service.AuthenticateAsync(It.Is<LoginRequest>(r =>
                    r.Email == request.Email && r.Password == request.Password)))
                .ReturnsAsync((AuthResponse?)null);

            // Act
            var result = await this.controller.Login(request);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid email or password", unauthorizedResult.Value);
        }
    }
}
