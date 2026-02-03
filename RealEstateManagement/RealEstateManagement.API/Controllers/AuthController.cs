using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateManagement.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : CustomControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                _logger.LogInformation("Login attempt for user: {Email}", loginDto.Email);
                var response = await _authService.LoginAsync(loginDto);
                _logger.LogInformation("Login successful for user: {Email}", loginDto.Email);
                return CreateResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for user: {Email}", loginDto.Email);
                throw;
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                _logger.LogInformation("Registration attempt for user: {Email}", registerDto.Email);
                var response = await _authService.RegisterAsync(registerDto);
                _logger.LogInformation("Registration successful for user: {Email}", registerDto.Email);
                return CreateResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed for user: {Email}", registerDto.Email);
                throw;
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            try
            {
                _logger.LogInformation("Token refresh attempt");
                var response = await _authService.RefreshTokenAsync(refreshTokenDto);
                _logger.LogInformation("Token refresh successful");
                return CreateResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token refresh failed");
                throw;
            }
        }
    }
}
