using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class CondicionTrabajo : CoreModel<int>
    {
        public string Descripcion { get; set; }
        public int IdTipoCondicionTrabajo { get; set; }

        public virtual TipoCondicionTrabajo TipoCondicionTrabajo { get; set; }

        public virtual ICollection<CondicionTrabajoPuesto> CondicionTrabajoPuestos { get; set; }
    }
}
