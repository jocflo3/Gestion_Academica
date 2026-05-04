namespace Servidor_API.Models
{
    public class HorarioMateria
    {
        public int IdHMateria { get; set; }
        public int IdMatDisp { get; set; }
        public int Dia { get; set; }
        public TimeOnly HoraIni { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}
