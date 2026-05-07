using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Servidor_API.Controllers;
using Servidor_API.Models;
using Servidor_API.Repositories;
using Servidor_API.Repositories.Interfaces;
using Servidor_API.Services.Interfaces;
using Servidor_DTOS.DTOS.Auth;
using Servidor_DTOS.DTOS.Usuario;
using System.Data;

namespace Servidor_API.Services
{
    public class UsuarioService: IUsuarioService
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task RegistraUsuario(RegistrarUsuarioDTO user)
        {
            var Roles = user.Roles.Select(r => new Rol
              {
                  IdRol = r.IdRol,
                  DescRol = ""
              }).ToList();
            var usuario = new Usuario
            {
                UserName = user.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                IdAlumno = user.IdAlumno,
                IdProfesor= user.IdProfesor,
                Activo = user.Activo,
                Roles = Roles
            };
            await _usuarioRepository.RegistraUsuario(usuario);
        }
        public async Task<List<ListUsuarioDTO>> ObtenerUsuarios(bool? SoloActivos)
        {
            var Usuarios = await _usuarioRepository.ObtenerUsuarios(SoloActivos);

            return Usuarios;
        }
    }
}
