using FinanceAPI.Models;
using FinanceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var user = await _authService.RegisterAsync(dto.Email, dto.Password, dto.Name);
                var token = _tokenService.GenerateToken(user);
                return Ok(new { Token = token, UserId = user.Id, user.Email, user.Name });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _authService.ValidateCredentialsAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token, UserId = user.Id, user.Email, user.Name });
        }

        // GET: api/auth/me  (example authenticated endpoint)
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return Ok(new { UserId = userId });
        }
    }

    // DTOs in the same file or in a separate folder (e.g., Dtos/)
    public class RegisterDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

    public class LoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
