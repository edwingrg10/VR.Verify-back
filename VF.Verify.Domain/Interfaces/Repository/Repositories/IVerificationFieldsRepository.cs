using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IVerificationFieldsRepository
    {
        Task<List<VerificationField>> GetByCriteriaAndSourceAsync(int? criteriaId, int sourceId);

    }
}
