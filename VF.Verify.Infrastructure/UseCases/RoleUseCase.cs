using VF.Verify.Application.Interfaces;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.Repository.Repositories;

namespace VF.Verify.Application.UseCases
{
    public class RoleUseCase : IRoleUseCase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleUseCase(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ResponseDTO> GetRolesAsync()
        {
            return await _roleRepository.GetRolesAsync();
        }

        public async Task<ResponseDTO> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetRoleByIdAsync(id);
        }

        public async Task<ResponseDTO> CreateRoleAsync(string roleName)
        {
            var role = new Role { Name = roleName };
            return await _roleRepository.CreateRoleAsync(role);
        }

        public async Task<ResponseDTO> UpdateRoleAsync(Role roleDto)
        {
            var role = new Role { Id = roleDto.Id, Name = roleDto.Name };
            return await _roleRepository.UpdateRoleAsync(role);
        }

        public async Task<ResponseDTO> DeleteRoleAsync(int id)
        {
            return await _roleRepository.DeleteRoleAsync(id);
        }
    }
}
