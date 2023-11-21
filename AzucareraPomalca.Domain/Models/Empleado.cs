using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Empleado : CoreModel<int>
    {
        public string Nombres { get; set; }
        public string AppellidoPaterno { get; set; }
        public string AppellidoMaterno { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public int IdCondicionEmpleado { get; set; }
        public string? CodigoCardo { get; set; }
        public string? CodigoArea { get; set; }
        public int IdPuesto { get; set; }

        public virtual Puesto Puesto { get; set; }
    }
}
