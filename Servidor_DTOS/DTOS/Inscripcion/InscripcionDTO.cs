using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Inscripcion
{
    public class InscripcionDTO
    {
        public int IdAlumno { get; set; }
        public int IdMatDisp { get; set; }
        public int Calificacion { get; set; }
        public int Oportunidad { get; set; }
        public bool Acreditada { get; set; }
    }
}
