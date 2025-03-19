using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;
using VF.Verify.Domain.Interfaces.UseCases;
using VF.Verify.Infrastructure.Repository.Repositories;

namespace VF.Verify.Infrastructure.UseCases
{
    public class DistribuitorUseCase : IDistribuitorUseCase
    {
        private readonly IDistribuitorRepository _distribuitorRepository;

        public DistribuitorUseCase(IDistribuitorRepository distribuitorRepository)
        {
            _distribuitorRepository = distribuitorRepository;
        }
        public async Task<ResponseDTO> CreateDistribuitorAsync(Distributor distribuitor)
        {
            return await _distribuitorRepository.CreateDistribuitorAsync(distribuitor);
        }

        public async Task<ResponseDTO> DeleteDistribuitorAsync(int id)
        {
            return await _distribuitorRepository.DeleteDistribuitorAsync(id);
        }

        public async Task<ResponseDTO> GetDistribuitorByIdAsync(int id)
        {
            return await _distribuitorRepository.GetDistribuitorByIdAsync(id);
        }

        public async Task<ResponseDTO> GetDistribuitorsAsync()
        {
            return await _distribuitorRepository.GetDistribuitorsAsync();
        }

        public async Task<ResponseDTO> UpdateDistribuitorAsync(Distributor distribuitor)
        {
            return await _distribuitorRepository.UpdateDistribuitorAsync(distribuitor);
        }
    }
}
