using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Empleado : CoreModel<int>
    {
        public string Nombres { get; set; }
        public string? NumeroDocumento { get; set; }
        public string AppellidoPaterno { get; set; }
        public string AppellidoMaterno { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public int IdCondicionEmpleado { get; set; }
        public int IdPuesto { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdSexo { get; set; }
        public int? IdTipoDocumentoIdentidad { get; set; }
        public DateTime? FinPeriodo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }

        public virtual Puesto Puesto { get; set; }
        public virtual CondicionEmpleado CondicionEmpleado { get; set; }
        public virtual TablaComun EstadoCivil { get; set; }
        public virtual TablaComun Sexo { get; set; }
        public virtual TablaComun TipoDocumentoIdentidad { get; set; }

        public virtual ICollection<EmpleadoProfesion> EmpleadoProfesiones { get; set; }
        public virtual ICollection<ExperienciaLaboral> ExperienciaLaborales { get; set; }
        public virtual ICollection<EmpleadoCurso> EmpleadoCursos { get; set; }
        public virtual ICollection<PerfilCompetenciaEmpleado> PerfilCompetenciaEmpleados { get; set; }
        public virtual ICollection<CapacitacionEmpleado> CapacitacionEmpleados { get; set; }
    }
}
