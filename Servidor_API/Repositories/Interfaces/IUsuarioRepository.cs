using Servidor_API.Models;
using Servidor_DTOS.DTOS.Usuario;

namespace Servidor_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task RegistraUsuario(Usuario user);
        Task<List<ListUsuarioDTO>> ObtenerUsuarios(bool? SoloActivos);
        Task<bool> EliminaUsuario(int idUser);
        Task<bool> ActualizaUsuario(int idUser, Usuario usuario);
    }
}
