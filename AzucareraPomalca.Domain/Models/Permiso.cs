using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Permiso : CoreModel<int>
    {
        public bool Consultar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public int IdMenu { get; set; }
        public int IdRol { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
