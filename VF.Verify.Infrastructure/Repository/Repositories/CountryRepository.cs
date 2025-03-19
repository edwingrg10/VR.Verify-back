using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class CountryRepository : IContryRepository
    {

        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO> CreateCountryAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Pais creado exitosamente",
                Data = country
            };
        }

        public async Task<ResponseDTO> DeleteCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Pais no encontrado" };
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Pais eliminado exitosamente",
                Data = country
            };
        }

        public async Task<ResponseDTO> GetCountriesAsync()
        {
            var countries = await _context.Countries.ToListAsync();
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Paises obtenidos exitosamente",
                Data = countries
            };
        }

        public async Task<ResponseDTO> GetCountryByIdAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Pais no encontrado" };
            }

            return new ResponseDTO { IsSuccess = true, Message = "Pais obtenido exitosamente", Data = country };
        }

        public async Task<ResponseDTO> UpdateCountryAsync(Country country)
        {
            var existingCountry = await _context.Countries.FindAsync(country.Id);
            if (existingCountry == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Pais no encontrado" };
            }

            existingCountry.Name = country.Name;
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Pais actualizado exitosamente",
                Data = existingCountry
            };
        }
    }
}
