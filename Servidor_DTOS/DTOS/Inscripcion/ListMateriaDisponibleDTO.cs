using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Inscripcion
{
    public class ListMateriaDisponibleDTO
    {
        public int IdMatDisp { get; set; }
        public string? CveMateria { get; set; }
        public int TipoMateria { get; set; }//Para guardar si es complementaria o del plan de estudios
        public string CveProfe { get; set; }
        public DateTime FhIni { get; set; }
        public DateTime FhFin { get; set; }
        public int IdEstado { get; set; }
        public int IdPeriodo { get; set; }
        public int Cupo { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
