using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Personas
{
    public class ProfesorDTO
    {
        public string Nombre { get; set; }
        public string PrimerAp { get; set; }
        public string? SegundoAp { get; set; }
        public string IdCarrera { get; set; }
        public int EsTutor { get; set; }
        public int IdEstado { get; set; }
    }
}
