using Servidor_API.Services.Interfaces;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using Servidor_API.Models;
using Servidor_API.Repositories.Interfaces;
using Servidor_DTOS.DTOS.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Servidor_API.Services
{
    public class AuthService : IAuthService
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration){
            _configuration = configuration;
            _usuarioRepository= usuarioRepository;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            var usuario = await _usuarioRepository.ObtenerPorUsername(request.Username);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            if (string.IsNullOrWhiteSpace(usuario.PasswordHash))
            {
                throw new Exception("Contraseña requerida");
            }
            if (!usuario.Activo)
            {
                throw new Exception("Usuario no activo");
            }
            var passwordValido = BCrypt.Net.BCrypt.Verify(request.Password,usuario.PasswordHash);

            if (!passwordValido)
            {
                throw new Exception("Contraseña incorrecta");
            }
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]));

            var token =  GenerarToken(usuario, expiration);

            var refreshToken = GenerarRefreshToken();

            var refreshExpiration = DateTime.Now.AddDays(7);

            await _usuarioRepository.ActualizarRefreshToken(usuario.idUsuario,refreshToken, refreshExpiration);

            return new LoginResponseDTO
            {
                Token = token,
                Expiration = expiration,
                RefreshToken = refreshToken,
                Username = usuario.UserName,
                Rol = usuario.Rol
            };
        }

        private string GenerarToken(
            Usuario usuario,
            DateTime expiration)
        {
            var jwtSettings =
                _configuration.GetSection("Jwt");

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtSettings["Key"]));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.idUsuario.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    usuario.UserName),

                new Claim(
                    ClaimTypes.Role,
                    usuario.Rol)
            };

            var token =
                new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        private string GenerarRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
