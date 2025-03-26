using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class DistribuitorRepository : IDistribuitorRepository
    {
        private readonly ApplicationDbContext _context;

        public DistribuitorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO> CreateDistribuitorAsync(Distributor distribuitor)
        {
            _context.Distribuitors.Add(distribuitor);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Distribuidor creado exitosamente",
                Data = distribuitor
            };
        }

        public async Task<ResponseDTO> DeleteDistribuitorAsync(int id)
        {
            var distributor = await _context.Distribuitors.FindAsync(id);
            if (distributor == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Distribuidor no encontrado" };
            }

            _context.Distribuitors.Remove(distributor);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Distribuidor eliminado exitosamente",
                Data = distributor
            };
        }

        public async Task<ResponseDTO> GetDistribuitorByIdAsync(int id)
        {
            var distributor = await _context.Distribuitors.FindAsync(id);
            if (distributor == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Distribuidor no encontrado" };
            }

            return new ResponseDTO { IsSuccess = true, Message = "Distribuidor obtenido exitosamente", Data = distributor };
        }

        public async Task<ResponseDTO> GetDistribuitorsAsync()
        {
            var distributors = await _context.Distribuitors.ToListAsync();
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Distribuidores obtenidos exitosamente",
                Data = distributors
            };
        }

        public async Task<ResponseDTO> UpdateDistribuitorAsync(Distributor distribuitor)
        {
            var existingDistribuitor = await _context.Distribuitors.FindAsync(distribuitor.Id);
            if (existingDistribuitor == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Distribuidor no encontrado" };
            }

            existingDistribuitor.Name = distribuitor.Name;
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Distribuidor actualizado exitosamente",
                Data = existingDistribuitor
            };
        }

        public async Task<bool> ExistsAsync(int distributorId)
        {
            return await _context.Distribuitors.AnyAsync(d => d.Id == distributorId);
        }
    }
}
