namespace Servidor_API.Models
{
    public class Inscripcion
    {
        public int IdInsc { get; set; }
        public int IdAlumno {  get; set; }
        public int IdMatDisp {  get; set; }
        public int Calificacion { get; set; }
        public int Oportunidad { get; set; }
        public bool Acreditada { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
