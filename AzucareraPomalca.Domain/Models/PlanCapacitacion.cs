using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class PlanCapacitacion : CoreModel<int>
    {
        public int Anio { get; set; }
        public string? Descripcion { get; set; }
        public decimal PresupuestoTotal { get; set; }
        public int FuerzaLaboral { get; set; }
        public int IdEstadoPlan { get; set; }
        public DateTime? FechaAprobacion { get; set; }

        public virtual TablaComun EstadoPlan { get; set; }
        public virtual ICollection<Capacitacion> Capacitaciones { get; set; }
    }
}
