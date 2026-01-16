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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            return CreateResult(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);
            return CreateResult(response);
        }
    }
}
