using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.Include(c => c.Distributor).ToListAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies.Include(c => c.Distributor).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<bool> DeleteCompanyAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return false;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company;
        }
    }


}
