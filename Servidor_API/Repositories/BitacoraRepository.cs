using Servidor_API.Repositories.Interfaces;
using Dapper;

namespace Servidor_API.Repositories
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly DapperContext _context;

        public BitacoraRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task RegistrarError(string mensaje, string stackTrace, string endpoint, string metodo, string? usuario, string? ip)
        {
            var sql = @"
            INSERT INTO BITACORA
            (
                MENSAJE,
                STACK_TRACE,
                ENDPOINT,
                METODO,
                USUARIO,
                IP_USER
            )
            VALUES
            (
                @Mensaje,
                @StackTrace,
                @Endpoint,
                @Metodo,
                @Usuario,
                @IP
            )";

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                Mensaje = mensaje,
                StackTrace = stackTrace,
                Endpoint = endpoint,
                Metodo = metodo,
                Usuario = usuario,
                IP      = ip
            });
        }
    }
}
