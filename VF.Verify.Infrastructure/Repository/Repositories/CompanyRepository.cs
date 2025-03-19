using Microsoft.EntityFrameworkCore;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDTO> GetCompaniesAsync()
    {
        try
        {
            var companies = await _context.Companies.ToListAsync();
            return new ResponseDTO { IsSuccess = true, Data = companies };
        }
        catch (Exception ex)
        {
            return new ResponseDTO { IsSuccess = false, Message = ex.Message };
        }
    }

    public async Task<ResponseDTO> GetCompanyByIdAsync(int id)
    {
        try
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return new ResponseDTO { IsSuccess = false, Message = "Compañia no encontrada" };

            return new ResponseDTO { IsSuccess = true, Data = company };
        }
        catch (Exception ex)
        {
            return new ResponseDTO { IsSuccess = false, Message = ex.Message };
        }
    }

    public async Task<ResponseDTO> CreateCompanyAsync(CreateCompanyDTO companyDto)
    {
        var company = new Company
        {
            Nit = companyDto.Nit,
            Name = companyDto.Name,
            ContactEmail = companyDto.ContactEmail,
            ContactFullName = companyDto.ContactFullName,
            DistributorId = companyDto.DistributorId,
            IsDistributor = companyDto.IsDistributor,
            IsActive = companyDto.IsActive
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        return new ResponseDTO { IsSuccess = true, Message = "Company creada exitosamente", Data = company };
    }

    public async Task<ResponseDTO> UpdateCompanyAsync(UpdateCompanyDTO companyDto)
    {
        var existingCompany = await _context.Companies.FindAsync(companyDto.Id);

        if (existingCompany == null)
        {
            return new ResponseDTO { IsSuccess = false, Message = "Compañia no encontrada" };
        }

        var distributorExists = await _context.Distribuitors.AnyAsync(d => d.Id == companyDto.DistributorId);
        if (!distributorExists)
        {
            return new ResponseDTO { IsSuccess = false, Message = "El Distributor especificado no existe" };
        }

        existingCompany.Nit = companyDto.Nit;
        existingCompany.Name = companyDto.Name;
        existingCompany.ContactEmail = companyDto.ContactEmail;
        existingCompany.ContactFullName = companyDto.ContactFullName;
        existingCompany.DistributorId = companyDto.DistributorId;
        existingCompany.IsDistributor = companyDto.IsDistributor;
        existingCompany.IsActive = companyDto.IsActive;

        _context.Companies.Update(existingCompany);
        await _context.SaveChangesAsync();

        return new ResponseDTO { IsSuccess = true, Message = "Company actualizada exitosamente", Data = existingCompany };
    }

    public async Task<ResponseDTO> DeleteCompanyAsync(int id)
    {
        try
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return new ResponseDTO { IsSuccess = false, Message = "Compañia no encontrada" };

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return new ResponseDTO { IsSuccess = true, Message = "Company eliminada exitosamente" };
        }
        catch (Exception ex)
        {
            return new ResponseDTO { IsSuccess = false, Message = ex.Message };
        }
    }
}
