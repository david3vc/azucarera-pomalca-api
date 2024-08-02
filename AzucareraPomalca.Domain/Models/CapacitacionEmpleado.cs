using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class CapacitacionEmpleado : CoreModel<int>
    {
        public int IdCapacitacion { get; set; }
        public int IdEmpleado { get; set; }
        public bool? Aprobado { get; set; }

        public virtual Capacitacion Capacitacion { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
