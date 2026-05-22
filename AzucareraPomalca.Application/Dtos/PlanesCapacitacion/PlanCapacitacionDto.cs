using AzucareraPomalca.Application.Dtos.Capacitaciones;
using AzucareraPomalca.Application.Dtos.TablaComunes;

namespace AzucareraPomalca.Application.Dtos.PlanesCapacitacion
{
    public class PlanCapacitacionDto
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string? Descripcion { get; set; }
        public decimal PresupuestoTotal { get; set; }
        public int FuerzaLaboral { get; set; }
        public int IdEstadoPlan { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public TablaComunDto? EstadoPlan { get; set; }

        // Sólo se llenan en el detalle (FindById); en listados quedan null.
        public List<CapacitacionDto>? Capacitaciones { get; set; }
        public PlanResumenDto? Resumen { get; set; }
    }
}
