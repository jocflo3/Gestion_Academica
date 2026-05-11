using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_DTOS.DTOS
{
    public class ListBitacoraDTO
    {
        public int IdBit { get; set; }
        public string Mensaje { get; set; }
        public string Traza { get; set; }
        public string Usuario { get; set; }
        public string Metodo { get; set; }
        public string Ip { get; set; }
        public string EndPoint { get; set; }
        public DateTime FhAlta { get; set; }
    }
}
