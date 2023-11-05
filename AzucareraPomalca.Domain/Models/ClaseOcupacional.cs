using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class ClaseOcupacional : CoreModel<int>
    {
        public string Nombre { get; set; }
        public int IdGrupoOcupacional { get; set; }

        public virtual GrupoOcupacional GrupoOcupacional { get; set; }
        public virtual ICollection<Puesto>? Puestos { get; set; }
    }
}
