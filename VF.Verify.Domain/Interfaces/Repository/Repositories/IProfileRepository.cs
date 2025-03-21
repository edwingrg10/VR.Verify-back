using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.Verify.Domain.Entities;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IProfileRepository
    {
        Task<List<Profile>> GetAllAsync();
        Task<Profile> GetByIdWithSourcesAsync(int id);
    }
}
