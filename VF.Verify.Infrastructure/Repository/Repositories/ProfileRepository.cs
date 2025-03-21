using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Profile>> GetAllAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile> GetByIdWithSourcesAsync(int id)
        {
            return await _context.Profiles
                .Include(p => p.ProfileSources)
                    .ThenInclude(ps => ps.Source)
                .Include(p => p.ProfileSources)
                    .ThenInclude(ps => ps.ConsultationCriteria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
