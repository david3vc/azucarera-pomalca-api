using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TomaDecision : CoreModel<int>
    {
        public string Descripcion { get; set; }
        public int IdTipoTomaDecision { get; set; }

        public virtual TipoTomaDecision TipoTomaDecision { get; set; }

        public virtual ICollection<TomaDecisionPuesto> TomaDecisionPuestos { get; set; }
    }
}
