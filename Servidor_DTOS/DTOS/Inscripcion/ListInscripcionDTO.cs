using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Inscripcion
{
    public class ListInscripcionDTO
    {
        public int IdInsc { get; set; }
        public string CvAlumno { get; set; }
        public string? NombreAlumno { get;set; }
        public string CvMatDisp { get; set; }
        public string? NombreMateria { get; set; }
        public int Calificacion { get; set; }
        public int Oportunidad { get; set; }
        public bool Acreditada { get; set; }
        public int TipoMateria { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
