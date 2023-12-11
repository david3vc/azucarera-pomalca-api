using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TipoTomaDecision : CoreModel<int>
    {
        public string Descripcion { get; set; }

        public virtual ICollection<TomaDecision> TomaDecisiones { get; set; }
    }
}
