namespace AzucareraPomalca.Application.Dtos.ExperienciaLaborales
{
    public class ExperienciaLaboralDto
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string TipoExperiencia { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
    }
}
