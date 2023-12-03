using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Profesion : CoreModel<int>
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdTipoProfesion { get; set; }

        public virtual TipoProfesion TipoProfesion { get; set; }

        public virtual ICollection<PuestoProfesion>? PuestosProfesiones { get; set; }
    }
}
