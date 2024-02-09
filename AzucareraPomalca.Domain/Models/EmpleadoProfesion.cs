using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class EmpleadoProfesion : CoreModel<int>
    {
        public int IdEmpleado { get; set; }
        public int IdProfesion { get; set; }
        public int? IdGradoAcademico { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Profesion Profesion { get; set; }
        public virtual GradoAcademico? GradoAcademico { get; set; }
    }
}
