namespace Servidor_API.Excepciones
{
    public class UnauthorizedException: BusinessException
    {
        public UnauthorizedException(string mensaje = "No autorizado")
            : base(
                mensaje,
                401,
                "UNAUTHORIZED")
        {
        }
    }
}
