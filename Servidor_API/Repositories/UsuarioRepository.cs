using Dapper;
using Servidor_API.Models;
using Servidor_API.Repositories.Interfaces;

namespace Servidor_API.Repositories
{
    public class UsuarioRepository: IUsuarioRepository
    {
        public readonly DapperContext _context;

        public UsuarioRepository(DapperContext context )
        {
            _context = context;
        }
        public async Task<Usuario?> ObtenerPorUsername(string username)
        {
            var sql = @"SELECT
                            ID_USUARIO AS IdUsuario,
                            USERNAME AS Username,
                            PASSWORD_HASH AS PasswordHash,
                            ACTIVO AS Activo
                        FROM USUARIO
                        WHERE USERNAME = @Username";
            using var connection = _context.CreateConnection();
            var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>(sql,new { Username = username });

            return usuario;
        }
        public async Task ActualizarRefreshToken(int idUsuario, string refreshToken, DateTime expiracion)
        {
            var sql = @"UPDATE USUARIO
                        SET
                            REFRESH_TOKEN = @RefreshToken,
                            REFRESH_TOKEN_EXP = @Expiracion
                        WHERE ID_USUARIO = @IdUsuario";

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                IdUsuario = idUsuario,
                RefreshToken = refreshToken,
                Expiracion = expiracion
            });
        }
    }
}
