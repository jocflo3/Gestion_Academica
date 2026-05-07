using Servidor_DTOS.DTOS.Usuario;

namespace Servidor_API.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task RegistraUsuario(RegistrarUsuarioDTO usuario);
    }
}
