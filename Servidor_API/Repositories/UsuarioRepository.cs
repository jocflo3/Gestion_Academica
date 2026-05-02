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
    }
}
