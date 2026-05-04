using System;

namespace Servidor_API.Models
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string UserName {  get; set; }
        public string Password { get; set; }
        public string? IdAlumno { get; set; }
        public string? IdProfesor { get; set; }
        public bool Activo { get; set; }
        public List<RolDTO>? Roles { get; set; }
        public DateTime? TokenExp { get; set; }
        public string? TokenRefresh { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
