using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Academia
{
    public class MateriaDTO
    {
        public string DescMateria { get; set; }
        public int IdEstado { get; set; }
        public int Creditos { get; set; }
        public int Semestre { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
    }
}
