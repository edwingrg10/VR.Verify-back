using Microsoft.AspNetCore.Mvc;
using VF.Verify.Application.Interfaces;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

namespace VF.Verify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleUseCase _roleUseCase;

        public RoleController(IRoleUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var response = await _roleUseCase.GetRolesAsync();
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _roleUseCase.GetRoleByIdAsync(id);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "El nombre del rol es obligatorio." });
            }

            var response = await _roleUseCase.CreateRoleAsync(roleName);
            return response.IsSuccess ? CreatedAtAction(nameof(GetRoleById), new { id = ((Role)response.Data).Id }, response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] Role roleDto)
        {
            var response = await _roleUseCase.UpdateRoleAsync(roleDto);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await _roleUseCase.DeleteRoleAsync(id);
            return response.IsSuccess ? Ok(response) : NotFound(response);
        }
    }
}
