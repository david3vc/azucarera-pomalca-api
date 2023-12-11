namespace AzucareraPomalca.Application.Dtos.TomaDecisionPuestos
{
    public class TomaDecisionPuestoSaveDto
    {
        public int? Id { get; set; }
        public int IdTomaDecision { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }
    }
}
