using Microsoft.AspNetCore.Mvc;
using SmartCampusResourceManagementAPI.DTOs;
using SmartCampusResourceManagementAPI.Repositories.Interfaces;
using SmartCampusResourceManagementAPI.Services;

namespace SmartCampusResourceManagementAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserAccountRepository userRepository, IJwtTokenService jwtTokenService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null || !PasswordHasher.Verify(request.Password, user.Password))
            {
                return Unauthorized(new { message = "Email hoac mat khau khong dung." });
            }

            var token = _jwtTokenService.GenerateToken(user);
            var expiryMinutes = int.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "30");

            return Ok(new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                Department = user.Department.ToString(),
                ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes)
            });
        }
    }
}
