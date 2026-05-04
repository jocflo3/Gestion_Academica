namespace Servidor_API.Models
{
    public class Carrera
    {
        public string IdCarrera { get; set; }
        public string DescCarrera { get; set; }
        public int Creditos { get; set; }
        public int CreditodCom {  get; set; }
        public int IdEstado {  get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
