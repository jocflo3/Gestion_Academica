namespace Servidor_API.Repositories.Interfaces
{
    public interface IBitacoraRepository
    {
         Task RegistrarError(string mensaje, string stackTrace, string endpoint, string metodo, string? usuario,string? ip);
    }
}
