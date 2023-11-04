using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Division : CoreModel<int>
    {
        public string Nombre { get; set; }
        public int? IdGerencia { get; set; }

        public virtual Gerencia? Gerencia { get; set; }
        public virtual ICollection<Departamento>? Departamentos { get; set; }
        public virtual ICollection<Seccion>? Secciones { get; set; }
    }
}
