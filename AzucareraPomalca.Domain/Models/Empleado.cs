using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Empleado : CoreModel<int>
    {
        public string Nombres { get; set; }
        public string? Dni { get; set; }
        public string AppellidoPaterno { get; set; }
        public string AppellidoMaterno { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public int IdCondicionEmpleado { get; set; }
        public string? CodigoCardo { get; set; }
        public string? CodigoArea { get; set; }
        public int IdPuesto { get; set; }

        public virtual Puesto Puesto { get; set; }
        public virtual CondicionEmpleado CondicionEmpleado { get; set; }

        public virtual ICollection<EmpleadoProfesion> EmpleadoProfesiones { get; set; }
        public virtual ICollection<ExperienciaLaboral> ExperienciaLaborales { get; set; }
        public virtual ICollection<EmpleadoCurso> EmpleadoCursos { get; set; }
        public virtual ICollection<PerfilCompetenciaEmpleado> PerfilCompetenciaEmpleados { get; set; }
    }
}
