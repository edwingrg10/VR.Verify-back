using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class VerificationFieldsRepository : IVerificationFieldsRepository
    {
        private readonly ApplicationDbContext _context;

        public VerificationFieldsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VerificationField>> GetByCriteriaAndSourceAsync(int criteriaId, int sourceId)
        {
            return await _context.VerificationFields
                .Include(vf => vf.Field)
                .Where(vf => vf.ConsultationCriteriaId == criteriaId && vf.SourceId == sourceId)
                .ToListAsync();
        }
    }
}
