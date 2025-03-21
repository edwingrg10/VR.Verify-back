using Microsoft.AspNetCore.Mvc;
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
            var profiles = await _profileUseCase.GetAllProfiles();
            return Ok(profiles);
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetProfileDetails(int id)
        {
            var result = await _profileUseCase.GetProfileDetails(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("criteria/{criteriaId}")]
        public async Task<IActionResult> GetCriteriaData(int criteriaId)
        {
            var result = await _profileUseCase.GetCriteriaData(criteriaId);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("verification-fields/{criteriaId}/{sourceId}")]
        public async Task<IActionResult> GetVerificationFields(int criteriaId, int sourceId)
        {
            var result = await _profileUseCase.GetVerificationFields(criteriaId, sourceId);
            return result.Any() ? Ok(result) : NotFound();
        }
    }
}
