namespace Servidor_API.Models
{
    public class Materia
    {
        public string IdMateria { get; set; }
        public string DescMateria { get; set; }
        public int IdEstado { get; set; }
        public  int Creditos { get; set; }
        public int Semestre {  get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
