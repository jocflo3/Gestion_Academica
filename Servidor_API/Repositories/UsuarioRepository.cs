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
