using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class GradoAcademico : CoreModel<int>
    {
        public string Descripcion { get; set; }

        public virtual ICollection<PuestoProfesion> PuestosProfesiones { get; set; }
        public virtual ICollection<EmpleadoProfesion>? EmpleadoProfesiones { get; set; }
    }
}
