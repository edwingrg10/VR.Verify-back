using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface IProfileUseCase
    {
        Task<List<ProfileDto>> GetAllProfiles();
        Task<ProfileDetailDto> GetProfileDetails(int profileId);
        Task<CriteriaDataDto> GetCriteriaData(int criteriaId);
        Task<List<FieldDto>> GetVerificationFields(int? criteriaId, int sourceId);
    }
}
