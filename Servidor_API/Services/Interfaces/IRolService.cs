using Servidor_DTOS.DTOS.Rol;

namespace Servidor_API.Services.Interfaces
{
    public interface IRolService
    {
        Task<RolDTO> ObtenerRoles();
        Task<bool> RegistraRol(string descs);
        Task<bool> EliminarRol(int idRol);
        Task<bool> ActualizaRol(int idRol, string desc);
    }
}
