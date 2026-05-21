using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class EmpleadoCurso : CoreModel<int>
    {
        public int IdCurso { get; set; }
        public int IdEmpleado { get; set; }
        public decimal HorasAcumuladas { get; set; }
        public DateTime? FechaCalculo { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
