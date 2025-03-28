﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistribuitorController : ControllerBase
    {
        private readonly IDistribuitorUseCase _distribuitorUseCase;

        public DistribuitorController(IDistribuitorUseCase distribuitorUseCase)
        {
            _distribuitorUseCase = distribuitorUseCase;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDistribuitors()
        {
            var response = await _distribuitorUseCase.GetDistribuitorsAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistribuitorById(int id)
        {
            var response = await _distribuitorUseCase.GetDistribuitorByIdAsync(id);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateDistributor(Distributor distributor)
        {
            var response = await _distribuitorUseCase.CreateDistribuitorAsync(distributor);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] Distributor distributor)
        {
            var response = await _distribuitorUseCase.UpdateDistribuitorAsync(distributor);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistributor(int id)
        {
            var response = await _distribuitorUseCase.DeleteDistribuitorAsync(id);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }
    }
}
