using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class GrupoOcupacional : CoreModel<int>
    {
        public string Nombre { get; set; }

        public virtual ICollection<ClaseOcupacional> ClaseOcupacionales { get; set; }
    }
}
