using Servidor_DTOS.DTOS.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Usuario
{
    public class ActualizaUsuarioDTO
    {
        public string UserName { get; set; }
        public string? IdAlumno { get; set; }
        public string? IdProfesor { get; set; }
        public bool Activo { get; set; }
        public List<RolDTO>? Roles { get; set; }
    }
}
