using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingTicektsManagment.Domain.Domains;
using ParkingTicektsManagment.Domain.Repositories;
using ParkingTicketsManagement.Infrastructure;
using ParkingTicketsManagment.DTOs.UserDTOs;
using ParkingTicketsManagment.Service;

namespace ParkingTicketsManagment.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly JWTTokenService _tokenProvider;

        public AuthController(IUnitOfWork uow, JWTTokenService tokenProvider)
        {
            _uow = uow;
            _tokenProvider = tokenProvider;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existingUser = await _uow.Auth.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email is already in use." });
            }
            string passwordHash = PassHasher.hashPassword(dto.Password);
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = Role.User
            };
            await _uow.Auth.AddAsync(newUser);
            await _uow.SaveChangesAsync();

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _uow.Auth.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            bool isPasswordValid = PassHasher.isValid(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            string token = _tokenProvider.Create(user);
            var response = new AuthResponseDto
            {
                Token = token,
                Email = user.Email
            };

            return Ok(response);
        }
    }
}
