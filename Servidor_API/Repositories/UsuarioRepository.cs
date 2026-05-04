using Dapper;
using Servidor_API.Models;
using Servidor_API.Repositories.Interfaces;
using System.Data.SqlTypes;

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
            if (usuario == null)
            {
                return null;
            }
            var sqlRol = @"SELECT
                             R.ID_ROL as IdRol,
                             R.DESC_ROL as DescRol
                         FROM USUARIO_ROL UR
                         INNER JOIN ROL R
                             ON R.ID_ROL = UR.ID_ROL
                         WHERE UR.ID_USUARIO = @IdUsuario";
            var Rol = await connection.QueryAsync<Rol>(sqlRol, new { IdUsuario = usuario.IdUsuario });
            usuario.Roles = Rol.ToList();
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

        public async Task RegistraUsuario(Usuario user)
        {

            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            { 
                var sql = @"INSERT INTO USUARIO(USERNAME,PASSWORD_HASH,NO_CONTROL,ID_PROFE,ACTIVO)
                        VALUES(
                            @User,
                            @Hash,
                            @NControl,
                            @IdProfe,
                            @Activo
                        );
                        SELECT CAST(SCOPE_IDENTITY() as int)";


                var userid = await connection.ExecuteScalarAsync(sql, new
                {
                    User = user.UserName,
                    Hash = user.PasswordHash,
                    NControl = user.IdAlumno,
                    IdProfe = user.IdProfesor,
                    Activo = user.Activo
                }, transaction);

                var sqlRol = @"INSERT INTO USUARIO_ROL(ID_USUARIO,ID_ROL)
                        VALUES(
                            @User,
                            @Rol
                        );";

                foreach (var rol in user.Roles)
                {
                    await connection.ExecuteAsync(
                        sqlRol,
                        new
                        {
                            User = userid,
                            Rol = rol.IdRol
                        }, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

        }
    }
}
