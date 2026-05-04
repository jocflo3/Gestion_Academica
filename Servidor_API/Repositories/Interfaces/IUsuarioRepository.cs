using Servidor_API.Models;

namespace Servidor_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorUsername(string username);
        Task ActualizarRefreshToken(int idUsuario,string refreshToken,DateTime expiracion);
        Task RegistraUsuario(Usuario user);
    }
}
