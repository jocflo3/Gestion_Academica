using Microsoft.AspNetCore.Mvc;
using Servidor_API.Models;
using Servidor_API.Services.Interfaces;
using Servidor_DTOS.DTOS.Auth;

namespace Servidor_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioService: ControllerBase
    {
        private readonly IUsuarioService _userService;

        public UsuarioService(IUsuarioService userService)
        {
            _userService = userService;
        }

        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> RegistraUsuario(UsuarioDTO user)
        {
            try
            {
                await _userService.RegistraUsuario(user);
                return Ok("Registrado correctamente");
            }
            catch (Exception ex) {
                return BadRequest("Ocurrio un error al registrar: "+ex.Message);
            }
        }
    }
}
