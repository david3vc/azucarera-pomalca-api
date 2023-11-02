using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TipoProfesion : CoreModel<int>
    {
        public string Descripcion { get; set; }

        public virtual ICollection<Profesion> Profesiones { get; set; }
    }
}
