using Microsoft.AspNetCore.Mvc;
using Servidor_DTOS.DTOS.Personas;

namespace Servidor_API.Services.Interfaces
{
    public interface IProfesorService
    {
        Task<ListProfesorDTO> ObtenerProfesore();
        Task<string> RegistraProfe(ProfesorDTO profe);
        Task<bool> EliminarProfe(string cvprofe);
        Task<bool> ActualizaProfe(string cvprofe, ProfesorDTO profe);
    }
}
