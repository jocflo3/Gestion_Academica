using Servidor_API.Models;

namespace Servidor_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorUsername(string username);

    }
}
