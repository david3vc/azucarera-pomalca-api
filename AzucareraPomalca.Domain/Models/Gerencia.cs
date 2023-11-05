using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Gerencia : CoreModel<int>
    {
        public string Nombre { get; set; }

        public virtual ICollection<Division>? Divisiones { get; set; }
        public virtual ICollection<Departamento>? Departamentos { get; set; }
        public virtual ICollection<Seccion>? Secciones { get; set; }
        public virtual ICollection<Puesto>? Puestos { get; set; }
    }
}
