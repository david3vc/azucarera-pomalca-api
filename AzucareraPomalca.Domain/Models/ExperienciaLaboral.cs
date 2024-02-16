using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class ExperienciaLaboral : CoreModel<int>
    {
        public string Empresa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string TipoExperiencia { get; set; }
        public int IdEmpleado { get; set; }

        public virtual Empleado Empleado { get; set; }
    }
}
