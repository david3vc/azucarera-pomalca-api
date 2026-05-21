using AzucareraPomalca.Application.Dtos.GradoDominios;

namespace AzucareraPomalca.Application.Dtos.PerfilCompetenciaEmpleados
{
    public class PerfilCompetenciaEmpleadoDto
    {
        public int Id { get; set; }
        public int? IdGradoDominio { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCompetencia { get; set; }
        public decimal HorasReales { get; set; }
        public decimal HorasEquivalentes { get; set; }
        public int? Nivel { get; set; }
        public string? NombreCompetencia { get; set; }
        public DateTime? FechaCalculo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public GradoDominioDto? GradoDominio { get; set; }
    }
}
