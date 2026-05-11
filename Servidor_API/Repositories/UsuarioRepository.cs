using Dapper;
using Microsoft.Data.SqlClient;
using Servidor_API.Excepciones;
using Servidor_API.Models;
using Servidor_API.Repositories.Interfaces;
using Servidor_DTOS.DTOS.Usuario;
using System.Data;
using System.Data.Common;
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
            await ValidaUsuario(user.UserName, connection,false);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            var sql = @"INSERT INTO USUARIO(USERNAME,PASSWORD_HASH,NO_CONTROL,ID_PROFE,ACTIVO)
                    VALUES(
                        @User,
                        @Hash,
                        @NControl,
                        @IdProfe,
                        @Activo
                    );
                    SELECT CAST(SCOPE_IDENTITY() as int)";


            var userid = await connection.ExecuteScalarAsync<int>(sql, new
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

            var rowsAffected = 0;
            foreach (var rol in user.Roles)
            {
                rowsAffected = await connection.ExecuteAsync(
                    sqlRol,
                    new
                    {
                        User = userid,
                        Rol = rol.IdRol
                    }, transaction);
                
            }
            transaction.Commit();
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

        public async Task EliminaUsuario(int idUser)
        {//Solo desactivo el usuario para mantener concordancia con la bitacora.           
            var sql = @"UPDATE USUARIO SET ACTIVO = 0, FH_MOD = GETDATE() WHERE ID_USUARIO = @idUser AND ACTIVO = 1";

            using var connection = _context.CreateConnection();

            await ValidaUsuario(idUser, connection);

            await connection.ExecuteAsync(sql, new { idUser = idUser });

        }
        public async Task ActualizaUsuario(int idUser, Usuario usuario)
        {//Solo desactivo el usuario para mantener concordancia con la bitacora.
            var sql = @"UPDATE USUARIO SET ACTIVO = 0, FH_MOD = GETDATE() WHERE ID_USUARIO = @idUser AND ACTIVO = 1";

            using var connection = _context.CreateConnection();

            await ValidaUsuario(idUser, connection);

            await connection.ExecuteAsync(sql, new { idUser = idUser });
        }

        public async Task ValidaUsuario<T>(T Campo, IDbConnection _connection,bool porId=true)
        {
            var sql = ""; var rowsAffected = 0;
            if (porId)
            {
                sql = @"SELECT COUNT(*) FROM USUARIO WHERE ID_USUARIO = @idUser";
                rowsAffected = await _connection.ExecuteScalarAsync<int>(sql, new { idUser = Campo });
            }
            else 
            { 
                sql = @"SELECT COUNT(*) FROM USUARIO WHERE UPPER(USERNAME) = UPPER(@userName)";
                rowsAffected = await _connection.ExecuteScalarAsync<int>(sql, new { userName = Campo });
            }


            if (rowsAffected == 0)
            {
                throw new NotFoundException("Usuario", Campo);
            }
        }
    }
}
