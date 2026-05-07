using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servidor_DTOS.DTOS.Rol;

namespace Servidor_DTOS.DTOS.Usuario
{
    public class RegistrarUsuarioDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? IdAlumno { get; set; }
        public string? IdProfesor { get; set; }
        public bool Activo { get; set; }
        public List<RolDTO>? Roles { get; set; }
    }
}
