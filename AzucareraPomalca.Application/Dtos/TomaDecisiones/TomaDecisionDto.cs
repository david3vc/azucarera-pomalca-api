using AzucareraPomalca.Application.Dtos.TipoTomaDecisiones;

namespace AzucareraPomalca.Application.Dtos.TomaDecisiones
{
    public class TomaDecisionDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoTomaDecision { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public TipoTomaDecisionDto TipoTomaDecision { get; set; }
    }
}
