using Servidor_API.Models;

namespace Servidor_API.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task RegistraUsuario(UsuarioDTO usuario);
    }
}
