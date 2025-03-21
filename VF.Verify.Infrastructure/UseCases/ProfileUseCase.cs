using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.UseCases;
using VF.Verify.Domain.DTOs;

namespace VF.Verify.Infrastructure.UseCases
{
    public class ProfileUseCase : IProfileUseCase
    {
        private readonly IProfileRepository _profileRepo;
        private readonly IConsultationCriteriaRepository _criteriaRepo;
        private readonly IVerificationFieldsRepository _verificationRepo;

        public ProfileUseCase(
            IProfileRepository profileRepo,
            IConsultationCriteriaRepository criteriaRepo,
            IVerificationFieldsRepository verificationRepo)
        {
            _profileRepo = profileRepo;
            _criteriaRepo = criteriaRepo;
            _verificationRepo = verificationRepo;
        }

        public async Task<List<ProfileDto>> GetAllProfiles()
        {
            var profiles = await _profileRepo.GetAllAsync();
            return profiles.Select(p => new ProfileDto(p.Id, p.Name)).ToList();
        }

        public async Task<ProfileDetailDto> GetProfileDetails(int profileId)
        {
            var profile = await _profileRepo.GetByIdWithSourcesAsync(profileId);
            if (profile == null) return null;

            var dto = new ProfileDetailDto
            {
                ProfileId = profile.Id,
                HasCriteria = profile.ProfileSources.Any(ps => ps.ConsultationCriteriaId.HasValue),
                Sources = profile.ProfileSources.Select(ps => new ProfileSourceDto(
                    ps.SourceId,
                    ps.ConsultationCriteriaId
                )).ToList()
            };

            return dto;
        }

        public async Task<CriteriaDataDto> GetCriteriaData(int criteriaId)
        {
            var criteria = await _criteriaRepo.GetByIdWithFieldsAsync(criteriaId);
            if (criteria == null) return null;

            return new CriteriaDataDto
            {
                CriteriaId = criteria.Id,
                CriteriaName = criteria.Name,
                Fields = criteria.VerificationFields
                    .Where(vf => vf.Field != null)
                    .Select(vf => new FieldDto(
                        vf.Field?.Id ?? 0,
                        vf.Field?.Name ?? "Campo desconocido",
                        vf.Field?.Type.ToString() ?? "TEXT",
                        vf.Field?.Metadata?.ToString() ?? null
                    ))
                    .ToList()
            };
        }

        public async Task<List<FieldDto>> GetVerificationFields(int? criteriaId, int sourceId)
        {
            var verificationFields = await _verificationRepo.GetByCriteriaAndSourceAsync(criteriaId, sourceId);

            return verificationFields
                .Where(vf => vf.Field != null)
                .Select(vf => new FieldDto(
                    vf.Field.Id,
                    vf.Field.Name,
                    vf.Field.Type.ToString(),
                    vf.Field?.Metadata?.ToString() ?? null
                ))
                .ToList();
        }
    }
}
