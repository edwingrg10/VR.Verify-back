using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.Verify.Domain.DTOs;

namespace VF.Verify.Domain.Interfaces.UseCases
{
    public interface IRuleUseCase
    {
        Task<ResponseDTO> ProcessRules(int companyCountryId, IFormFile excelFile);
    }

}
