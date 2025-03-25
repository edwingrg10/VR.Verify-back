using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VF.Verify.Domain.DTOs;
using VF.Verify.Domain.Entities;
using VF.Verify.Domain.Interfaces.UseCases;

namespace VF.Verify.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;

        public UserController(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userUseCase.GetUsersAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Obtener usuario por ID
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userUseCase.GetUserByIdAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Crear un nuevo usuario
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            if (userDto == null || string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Datos inválidos" });
            }

            var result = await _userUseCase.CreateUserAsync(userDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Actualizar usuario existente
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            if (id <= 0 || userDto == null)
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Datos inválidos" });
            }

            userDto.Id = id;
            var result = await _userUseCase.UpdateUserAsync(userDto);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Eliminar usuario por ID
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userUseCase.DeleteUserAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Obtener usuario por correo electrónico
        /// </summary>
        [Authorize]
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Correo inválido" });
            }

            var result = await _userUseCase.GetUserByEmailAsync(email);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Autenticar usuario (Login)
        /// </summary>
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest(new ResponseDTO { IsSuccess = false, Message = "Credenciales inválidas" });
            }

            var result = await _userUseCase.AuthenticateUserAsync(loginDto.Email, loginDto.Password);
            return result.IsSuccess ? Ok(result) : Unauthorized(result);
        }
    }

}
