namespace Servidor_API.Models
{
    public class Alumno
    {
        public string IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string PrimerAp { get; set; }
        public string? SegundoAp { get; set; }
        public string IdCarrera {  get; set; }
        public int Semestre { get; set;}
        public int IdTutor { get; set; }
        public int IdEstado { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
