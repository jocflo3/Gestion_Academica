namespace Servidor_API.Models
{
    public class Profesor
    {
        public string IdProfe { get; set; }
        public string Nombre {  get; set; }
        public string PrimerAp {  get; set; }
        public string? SegundoAp { get; set; }
        public string IdCarrera { get; set; }
        public int EsTutor { get; set; }
        public int IdEstado { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }

    }
}
