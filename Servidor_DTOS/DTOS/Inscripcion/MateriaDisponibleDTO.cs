using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Inscripcion
{
    public class MateriaDisponibleDTO
    {
        public string? CvMateria { get; set; }
        public string? CvCompl { get; set; }
        public string CvProfe { get; set; }
        public DateTime FhIni { get; set; }
        public DateTime FhFin { get; set; }
        public int IdEstado { get; set; }
        public int IdPeriodo { get; set; }
        public int Cupo { get; set; }
    }
}
