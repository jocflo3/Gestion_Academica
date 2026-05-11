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
            try
            {
                var usuario = await _userService.RegistraUsuario(user);
                if (usuario)
                {
                    return Ok($"Registrado correctamente");
                }
                else
                {
                    return NotFound("Usuario no registrado");
                }
            }
            catch (Exception ex) {
                return StatusCode(500, "Ocurrio un error al registrar: " +ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuario(bool? SoloActivos)
        {
            try
            {
                var Usuarios = await _userService.ObtenerUsuarios(SoloActivos);
                if (Usuarios!= null) {
                    return Ok(Usuarios);
                }
                else
                {
                    return NotFound();
                }  
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error al consultar: " + ex.Message);
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{username}")]
        public async Task<IActionResult> EliminaUsuario(int username)
        {
            try
            {
                var correcto = await _userService.EliminarUsuario(username);
                if (correcto)
                {
                    return Ok("Usuario eliminado correctamente");

                }
                else
                {
                    return NotFound("Usuario no eliminado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error al registrar: " + ex.Message);
            }
        }
        [HttpPut("{idUser}")]
        public async Task<IActionResult> ActualizarUsuario(int idUser,ActualizaUsuarioDTO user)
        {
            try
            {
                var correcto = await _userService.ActualizarUsuario(idUser, user);
                if (correcto)
                {
                    return Ok("Usuario actualizado correctamente");

                }
                else
                {
                    return NotFound("Usuario no actualizado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error al actualizar: " + ex.Message);
            }
        }
    }
}
