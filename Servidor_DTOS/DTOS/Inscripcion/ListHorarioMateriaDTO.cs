using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Inscripcion
{
    public class ListHorarioMateriaDTO
    {
        public int IdHMateria { get; set; }
        public string CvMatDisp { get; set; }
        public string DecMatDisp { get; set; }
        public string TipoMatDisp { get; set; }
        public int Dia { get; set; }
        public TimeOnly HoraIni { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}
