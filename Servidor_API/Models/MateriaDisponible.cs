namespace Servidor_API.Models
{
    public class MateriaDisponible
    {
        public int IdMatDisp { get; set; }
        public string? IdMateria { get; set; }
        public string? IdCompl { get; set; }
        public string IdProfe { get; set; }
        public DateTime FhIni { get; set; }
        public DateTime FhFin { get; set; }
        public int IdEstado {  get; set; }
        public int IdPeriodo { get; set; }
        public int Cupo { get; set; }
        public DateTime FhAlta { get; set; }
        public DateTime? FhMod { get; set; }
    }
}
