// <copyright file="IdentificationTypesController.cs" company="DITO SAS">
// Copyright (c) DITO SAS. All rights reserved.
// </copyright>

namespace UserManager.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using UserManager.API.Common.Responses;
    using UserManager.Infrastructure.Interfaces;

    /// <summary>
    /// Controller to manage identification types.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class IdentificationTypesController : ControllerBase
    {
        private readonly IIdentificationTypeRepository _repository;
        private readonly ILogger<IdentificationTypesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentificationTypesController"/> class.
        /// </summary>
        /// <param name="repository">Identification type repository.</param>
        /// <param name="logger">Logger instance.</param>
        public IdentificationTypesController(
            IIdentificationTypeRepository repository,
            ILogger<IdentificationTypesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all identification types.
        /// </summary>
        /// <returns>A list of identification types.</returns>
        [Authorize]
        [HttpGet("GetAllIdentificationType")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all type identifications");
            var types = await _repository.GetAllAsync();
            return Ok(types);
        }
    }
}
