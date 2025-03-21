using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class ConsultationCriteriaRepository : IConsultationCriteriaRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsultationCriteriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ConsultationCriteria> GetByIdWithFieldsAsync(int id)
        {
            return await _context.ConsultationCriterias
                .Include(cc => cc.VerificationFields)
                    .ThenInclude(vf => vf.Field)
                .FirstOrDefaultAsync(cc => cc.Id == id);
        }
    }
}
