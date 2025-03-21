using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Application.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileUseCase _profileUseCase;

        public ProfileController(IProfileUseCase profileUseCase)
        {
            _profileUseCase = profileUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            try
            {
                var profiles = await _profileUseCase.GetAllProfiles();
                return Ok(new ResponseDTO
                {
                    IsSuccess = true,
                    Message = "Perfiles obtenidos exitosamente",
                    Data = profiles
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetProfileDetails(int id)
        {
            try
            {
                var result = await _profileUseCase.GetProfileDetails(id);

                return result != null
                    ? Ok(new ResponseDTO
                    {
                        IsSuccess = true,
                        Message = "Detalles del perfil obtenidos",
                        Data = result
                    })
                    : NotFound(new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Perfil no encontrado"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }
        }

        [HttpGet("criteria/{criteriaId}")]
        public async Task<IActionResult> GetCriteriaData(int criteriaId)
        {
            try
            {
                var result = await _profileUseCase.GetCriteriaData(criteriaId);

                return result != null
                    ? Ok(new ResponseDTO
                    {
                        IsSuccess = true,
                        Message = "Criterio obtenido exitosamente",
                        Data = result
                    })
                    : NotFound(new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "Criterio no encontrado"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }
        }

        [HttpGet("verification-fields/{sourceId}")]
        public async Task<IActionResult> GetVerificationFields(
            [FromQuery] int? criteriaId,
            int sourceId)
        {
            try
            {
                var result = await _profileUseCase.GetVerificationFields(criteriaId, sourceId);

                return result.Any()
                    ? Ok(new ResponseDTO
                    {
                        IsSuccess = true,
                        Message = "Campos de verificación obtenidos",
                        Data = result
                    })
                    : NotFound(new ResponseDTO
                    {
                        IsSuccess = false,
                        Message = "No se encontraron campos de verificación"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}"
                });
            }
        }
    }
}