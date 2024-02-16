using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class PerfilCompetenciaEmpleado : CoreModel<int>
    {
        public int? IdGradoDominio { get; set; }
        public int IdEmpleado { get; set; }

        public virtual GradoDominio? GradoDominio { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
