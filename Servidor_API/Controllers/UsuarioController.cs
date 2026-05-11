using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servidor_API.Models;
using Servidor_API.Services.Interfaces;
using Servidor_DTOS.DTOS.Auth;
using Servidor_DTOS.DTOS.Usuario;

namespace Servidor_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioService: ControllerBase
    {
        private readonly IUsuarioService _userService;

        public UsuarioService(IUsuarioService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistraUsuario(RegistrarUsuarioDTO user)
        {
           
            await _userService.RegistraUsuario(user);
            return Ok("Registrado correctamente");
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuario(bool? SoloActivos)
        {
            var Usuarios = await _userService.ObtenerUsuarios(SoloActivos);
            return Ok(Usuarios);
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{userid}")]
        public async Task<IActionResult> EliminaUsuario(int userid)
        {
            await _userService.EliminarUsuario(userid);
            return Ok("Usuario eliminado correctamente");
        }
        [HttpPut("{idUser}")]
        public async Task<IActionResult> ActualizarUsuario(int idUser,ActualizaUsuarioDTO user)
        {
            await _userService.ActualizarUsuario(idUser, user);
            return Ok("Usuario actualizado correctamente");
        }
    }
}
