// <copyright file="IdentificationTypesController.cs" company="DITOS SAS">
// Copyright (c) DITOS SAS. All rights reserved.
// </copyright>

namespace UserManager.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UserManager.API.Common.Responses;
    using UserManager.Infrastructure.Interfaces;
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    public class IdentificationTypesController : ControllerBase
    {
        private readonly IIdentificationTypeRepository _repository;
        private readonly ILogger<IdentificationTypesController> _logger;

        public IdentificationTypesController(IIdentificationTypeRepository repository, ILogger<IdentificationTypesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

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
