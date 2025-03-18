using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Interfaces.Repository.Repositories
{
    public interface IExcelRepository
    {
        Task<List<string>> GetSheetNamesAsync(int companyId, int countryId);
    }
}
