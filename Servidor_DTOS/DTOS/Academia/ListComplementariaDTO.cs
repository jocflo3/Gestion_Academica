using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS.Academia
{
    public class ListComplementariaDTO
    {
        public string IdCompl { get; set; }
        public string DescCompl { get; set; }
        public int Creditos { get; set; }
        public int IdEstado { get; set; }
        public DateTime FhAlta { get; set; }
    }
}
