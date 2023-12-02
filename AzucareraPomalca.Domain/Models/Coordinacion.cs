using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Coordinacion : CoreModel<int>
    {
        public int IdPuestoCoordinador { get; set; }
        public int IdPuestoCoordinado { get; set; }

        public virtual Puesto PuestoCoordinador { get; set; }
        public virtual Puesto PuestoCoordinado { get; set; }
    }
}
