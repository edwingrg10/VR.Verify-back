using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class CompanyCountryRepository : ICompanyCountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyCountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO> AddCompanyToCountryAsync(int companyId, int countryId)
        {
            var companyExists = await _context.Companies.AnyAsync(c => c.Id == companyId);
            var countryExists = await _context.Countries.AnyAsync(c => c.Id == countryId);

            if (!companyExists || !countryExists)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Compañia o Pais no existen" };
            }

            var companyCountry = new CompanyCountry { CompanyId = companyId, CountryId = countryId };
            _context.CompanyCountries.Add(companyCountry);
            await _context.SaveChangesAsync();

            return new ResponseDTO { IsSuccess = true, Message = "Empresa asignada al país correctamente" };
        }

        public async Task<ResponseDTO> RemoveCompanyFromCountryAsync(int companyId, int countryId)
        {
            var companyCountry = await _context.CompanyCountries
                .FirstOrDefaultAsync(cc => cc.CompanyId == companyId && cc.CountryId == countryId);

            if (companyCountry == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "No se encontró la relación" };
            }

            _context.CompanyCountries.Remove(companyCountry);
            await _context.SaveChangesAsync();

            return new ResponseDTO { IsSuccess = true, Message = "Relación eliminada correctamente" };
        }

        public async Task<ResponseDTO> GetCompaniesByCountryAsync(int countryId)
        {
            var companies = await _context.CompanyCountries
                .Where(cc => cc.CountryId == countryId)
                .Select(cc => cc.Company)
                .ToListAsync();

            return new ResponseDTO { IsSuccess = true, Data = companies };
        }

        public async Task<ResponseDTO> GetCountriesByCompanyAsync(int companyId)
        {
            var countries = await _context.CompanyCountries
                .Where(cc => cc.CompanyId == companyId)
                .Select(cc => cc.Country)
                .ToListAsync();

            return new ResponseDTO { IsSuccess = true, Data = countries };
        }
    }

}
