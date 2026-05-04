namespace Servidor_API.Models
{
    public class Bitacora
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