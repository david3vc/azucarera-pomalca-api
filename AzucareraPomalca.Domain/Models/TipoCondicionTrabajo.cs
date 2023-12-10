using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TipoCondicionTrabajo : CoreModel<int>
    {
        public string Descripcion { get; set; }

        public virtual ICollection<CondicionTrabajo> CondicionTrabajos { get; set; }
    }
}
