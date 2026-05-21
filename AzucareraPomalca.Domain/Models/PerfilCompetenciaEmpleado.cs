using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class PerfilCompetenciaEmpleado : CoreModel<int>
    {
        public int? IdGradoDominio { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCompetencia { get; set; }
        public decimal HorasReales { get; set; }
        public decimal HorasEquivalentes { get; set; }
        public DateTime? FechaCalculo { get; set; }

        public virtual GradoDominio? GradoDominio { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual Competencia Competencia { get; set; }
    }
}
