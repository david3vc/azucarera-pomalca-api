using AzucareraPomalca.Application.Dtos.Niveles;
using AzucareraPomalca.Application.Dtos.TomaDecisiones;

namespace AzucareraPomalca.Application.Dtos.TomaDecisionPuestos
{
    public class TomaDecisionPuestoDto
    {
        public int Id { get; set; }
        public int IdTomaDecision { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public TomaDecisionDto TomaDecision { get; set; }
        public NivelDto? Nivel { get; set; }
    }
}
