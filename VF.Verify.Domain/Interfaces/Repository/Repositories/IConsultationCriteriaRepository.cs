using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IConsultationCriteriaRepository
    {
        Task<ConsultationCriteria> GetByIdWithFieldsAsync(int id);
    }
}
