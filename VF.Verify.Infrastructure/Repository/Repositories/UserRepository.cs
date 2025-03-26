using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Infrastructure.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ResponseDTO> GetUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Distributor)
                .Include(u => u.CompanyCountry)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.Password,
                    Rol = new { u.Role.Id, u.Role.Name },
                    Distributor = new { u.Distributor.Id, u.Distributor.Name },
                    CompanyCountry = new { u.CompanyCountry.Id }
                })
                .ToListAsync();

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Usuarios obtenidos",
                Data = users
            };
        }

        public async Task<ResponseDTO> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)          
                .Include(u => u.Distributor)  
                .Include(u => u.CompanyCountry) 
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.Password,
                    Rol = new { u.Role.Id, u.Role.Name },
                    Distributor = new { u.Distributor.Id, u.Distributor.Name },
                    CompanyCountry = new { u.CompanyCountry.Id }
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };
            }

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Usuario encontrado",
                Data = user
            };
        }

        public async Task<ResponseDTO> CreateUserAsync(UserDTO userDto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Email == userDto.Email);
            if (userExists)
            {
                return new ResponseDTO { IsSuccess = false, Message = "El usuario ya existe" };
            }

            var distributorExists = await _context.Distribuitors.AnyAsync(d => d.Id == userDto.DistributorId);
            if (!distributorExists)
            {
                return new ResponseDTO { IsSuccess = false, Message = "El Distributor especificado no existe" };
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new User
            {
                Email = userDto.Email,
                Password = hashedPassword,
                RolId = userDto.RolId,
                DistributorId = userDto.DistributorId,
                CompanyCountryId = userDto.CompanyCountryId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ResponseDTO { IsSuccess = true, Message = "Usuario creado exitosamente" };
        }

        public async Task<ResponseDTO> UpdateUserAsync(UserDTO userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
            if (user == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Usuario no encontrado" };
            }

            var distributorExists = await _context.Distribuitors.AnyAsync(d => d.Id == userDto.DistributorId);
            if (!distributorExists)
            {
                return new ResponseDTO { IsSuccess = false, Message = "El Distributor especificado no existe" };
            }

            user.Email = userDto.Email;
            user.RolId = userDto.RolId;
            user.DistributorId = userDto.DistributorId;
            user.CompanyCountryId = userDto.CompanyCountryId;

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            await _context.SaveChangesAsync();
            return new ResponseDTO { IsSuccess = true, Message = "Usuario actualizado correctamente" };
        }

        public async Task<ResponseDTO> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Usuario no encontrado" };
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new ResponseDTO { IsSuccess = true, Message = "Usuario eliminado correctamente" };
        }

        public async Task<ResponseDTO> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Distributor)
                .Include(u => u.CompanyCountry)
                .Where(u => u.Email == email)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.Password,
                    Rol = new { u.Role.Id, u.Role.Name },
                    Distributor = new { u.Distributor.Id, u.Distributor.Name },
                    CompanyCountry = new { u.CompanyCountry.Id }
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Usuario no encontrado"
                };
            }

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Usuario encontrado",
                Data = user
            };
        }
        public async Task<ResponseDTO> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Distributor)
                .Include(u => u.CompanyCountry)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Usuario o contraseña incorrectos" };
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!passwordValid)
            {
                return new ResponseDTO { IsSuccess = false, Message = "Usuario o contraseña incorrectos" };
            }

            var token = GenerateJwtToken(user);
            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Autenticación exitosa",
                Data = new
                {
                    Token = token,
                    User = new
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Role = new { user.Role.Id, user.Role.Name },
                        Distributor = new { user.Distributor.Id, user.Distributor.Name },
                        CompanyCountry = new { user.CompanyCountry.Id }
                    }
                }
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                new Claim("role", user.RolId.ToString())
            };
            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationInMinutes"]);
            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
