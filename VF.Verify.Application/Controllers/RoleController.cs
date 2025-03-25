using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleUseCase _roleUseCase;

    public RoleController(IRoleUseCase roleUseCase)
    {
        _roleUseCase = roleUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var response = await _roleUseCase.GetAllRoles();
        return GetActionResult(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var response = await _roleUseCase.GetRoleById(id);
        return GetActionResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateDTO roleDto)
    {
        var response = await _roleUseCase.CreateRole(roleDto);
        return GetActionResult(response, nameof(GetRoleById), response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole([FromBody] RoleUpdateDTO roleDto)
    {
        var response = await _roleUseCase.UpdateRole(roleDto);
        return GetActionResult(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var response = await _roleUseCase.DeleteRole(id);
        return GetActionResult(response);
    }

    [HttpPost("assign-permissions")]
    public async Task<IActionResult> AssignPermissions([FromBody] AssignPermissionsDTO request)
    {
        var response = await _roleUseCase.AssignPermissionsToRole(request);
        return GetActionResult(response);
    }

    [HttpGet("{id}/permissions")]
    public async Task<IActionResult> GetRolePermissions(int id)
    {
        var response = await _roleUseCase.GetRolePermissions(id);
        return GetActionResult(response);
    }   

    private IActionResult GetActionResult(ResponseDTO response, string actionName = null, object routeValues = null)
    {
        return response.IsSuccess
            ? (actionName != null
                ? CreatedAtAction(actionName, routeValues, response)
                : Ok(response))
            : BadRequest(response);
    }
}