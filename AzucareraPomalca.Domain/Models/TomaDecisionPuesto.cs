using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TomaDecisionPuesto : CoreModel<int>
    {
        public int IdTomaDecision { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }

        public virtual TomaDecision TomaDecision { get; set; }
        public virtual Puesto Puesto { get; set; }
        public virtual Nivel? Nivel { get; set; }
    }
}
