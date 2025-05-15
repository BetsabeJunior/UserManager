// <copyright file="IdentificationTypesControllerTests.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.Tests.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using UserManager.API.Controllers;
    using UserManager.Domain.Entities;
    using UserManager.Infrastructure.Interfaces;

    /// <summary>
    /// This class tests the IdentificationTypesControllerTests.
    /// </summary>
    public class IdentificationTypesControllerTests
    {
        private readonly Mock<IIdentificationTypeRepository> _mockRepo;
        private readonly Mock<ILogger<IdentificationTypesController>> _mockLogger;
        private readonly IdentificationTypesController _controller;

        public IdentificationTypesControllerTests()
        {
            _mockRepo = new Mock<IIdentificationTypeRepository>();
            _mockLogger = new Mock<ILogger<IdentificationTypesController>>();
            _controller = new IdentificationTypesController(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithIdentificationTypes()
        {
            var expected = new List<IdentificationType>
            {
                new IdentificationType { Id = 1, Code = "CC", Name = "Cedula" },
                new IdentificationType { Id = 2, Code = "PA", Name = "Pasaporte" },
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expected);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedTypes = Assert.IsAssignableFrom<IEnumerable<IdentificationType>>(okResult.Value);
            Assert.Equal(2, ((List<IdentificationType>)returnedTypes).Count);
        }
    }
}
