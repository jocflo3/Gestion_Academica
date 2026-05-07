using Servidor_API.Services.Interfaces;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using Servidor_API.Models;
using Servidor_API.Repositories.Interfaces;
using Servidor_DTOS.DTOS.Auth;
using Servidor_DTOS.DTOS.Rol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Servidor_API.Services
{
    public class AuthService : IAuthService
    {
        public readonly IAuthRepository _authRepository;
        public readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration){
            _configuration = configuration;
            _authRepository = authRepository;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            var usuario = await _authRepository.ObtenerPorUsername(request.Username);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new Exception("Contraseña requerida");
            }
            if (!usuario.Activo)
            {
                throw new Exception("Usuario no activo");
            }
            if (usuario.Roles == null)
            {
                throw new Exception("Usuario sin rol definido");
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

            await _authRepository.ActualizarRefreshToken(usuario.IdUsuario,refreshToken, refreshExpiration);

            return new LoginResponseDTO
            {
                Token = token,
                Expiration = expiration,
                RefreshToken = refreshToken,
                Username = usuario.UserName,
                Roles = usuario.Roles.Select(r => new RolDTO 
                {
                    IdRol = r.IdRol,
                    DescRol = r.DescRol
                }).ToList()
            };
        }

        private string GenerarToken(Usuario usuario,DateTime expiration)
        {
            var jwtSettings =
                _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,usuario.IdUsuario.ToString()),

                new Claim(ClaimTypes.Name,usuario.UserName),

            };
            foreach (var rol in usuario.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,rol.DescRol));
            }
            var token =
                new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerarRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
