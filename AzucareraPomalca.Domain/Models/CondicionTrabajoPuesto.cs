using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class CondicionTrabajoPuesto : CoreModel<int>
    {
        public bool IsMarked { get; set; }
        public int IdCondicionTrabajo { get; set; }
        public int IdPuesto { get; set; }

        public virtual CondicionTrabajo CondicionTrabajo { get; set; }
        public virtual Puesto Puesto { get; set; }
    }
}
