using AzucareraPomalca.Application.Dtos.GradoDominios;

namespace AzucareraPomalca.Application.Dtos.PerfilCompetencias
{
    public class PerfilCompetenciaDto
    {
        public int Id { get; set; }
        public int? IdGradoDominio { get; set; }
        public int IdPuesto { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public GradoDominioDto? GradoDominio { get; set; }
    }
}
