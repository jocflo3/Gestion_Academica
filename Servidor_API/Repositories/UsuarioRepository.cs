using Dapper;
using Servidor_API.Models;
using Servidor_API.Repositories.Interfaces;
using Servidor_DTOS.DTOS.Usuario;
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
        public async Task<List<ListUsuarioDTO>> ObtenerUsuarios(bool? SoloActivos)
        {
            var sql = @"SELECT 
	                        ID_USUARIO AS IdUsuario,
	                        USERNAME AS UserName,
	                        NO_CONTROL AS IdAlumno,
	                        ID_PROFE AS IdProfesor,
	                        ACTIVO AS Activo,
	                        FH_ALTA AS FhAlta,
	                        FH_MOD AS FhMod
                        FROM USUARIO WHERE ACTIVO = @Activo OR @Activo IS NULL";

            using var connection = _context.CreateConnection();

            var usuarios = await connection.QueryAsync<ListUsuarioDTO>(sql, new {Activo = SoloActivos});

            return usuarios.ToList();
        }

        public async Task<bool> EliminaUsuario(string username)
        {//Solo desactivo el usuario para mantener concordancia con la bitacora.
            try
            {
                var sql = @"UPDATE USUARIO SET ACTIVO = 0, FH_MOD = GETDATE() WHERE USERNAME = @Username AND ACTIVO = 1" ;

                using var connection = _context.CreateConnection();

                var rowsAffected =  await connection.ExecuteAsync(sql, new { Username = username });

                return rowsAffected > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
