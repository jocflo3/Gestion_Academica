using Microsoft.AspNetCore.Mvc;
using Servidor_API.Services.Interfaces;
using Servidor_DTOS.DTOS.Auth;

namespace Servidor_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            var response = await _authService.Login(request);

            return Ok(response);
        }
    }
}