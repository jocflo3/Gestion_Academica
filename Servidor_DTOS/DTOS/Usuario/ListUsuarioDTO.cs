using Servidor_DTOS.DTOS.Rol;
using System;

namespace Servidor_DTOS.DTOS.Usuario
{
    public class ListUsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string UserName {  get; set; }
        public string? IdAlumno { get; set; }
        public string? IdProfesor { get; set; }
        public bool Activo { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
