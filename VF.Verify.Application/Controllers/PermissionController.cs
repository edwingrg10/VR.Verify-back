using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.UseCases;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IPermissionUseCase _permissionUseCase;

    public PermissionController(IPermissionUseCase permissionUseCase)
    {
        _permissionUseCase = permissionUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        var response = await _permissionUseCase.GetAllPermissions();
        return HandleResponse(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPermissionById(int id)
    {
        var response = await _permissionUseCase.GetPermissionById(id);
        return HandleResponse(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePermission([FromBody] PermissionCreateDTO permissionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseDTO { Message = "Datos inválidos" });

        var response = await _permissionUseCase.CreatePermission(permissionDto);
        return HandleResponse(response, nameof(GetPermissionById), response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePermission([FromBody] PermissionUpdateDTO permissionDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseDTO { Message = "Datos inválidos" });

        var response = await _permissionUseCase.UpdatePermission(permissionDto);
        return HandleResponse(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePermission(int id)
    {
        var response = await _permissionUseCase.DeletePermission(id);
        return HandleResponse(response);
    }

    private IActionResult HandleResponse(ResponseDTO response, string actionName = null, object routeValues = null)
    {
        if (response.IsSuccess)
        {
            return actionName != null
                ? CreatedAtAction(actionName, routeValues, response)
                : Ok(response);
        }

        return response.Message.Contains("no encontrado")
            ? NotFound(response)
            : BadRequest(response);
    }
}