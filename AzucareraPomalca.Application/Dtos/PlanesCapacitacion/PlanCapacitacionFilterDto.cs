namespace AzucareraPomalca.Application.Dtos.PlanesCapacitacion
{
    public class PlanCapacitacionFilterDto
    {
        public int? Anio { get; set; }
        public int? IdEstadoPlan { get; set; }
        public string? Descripcion { get; set; }
        public decimal? PresupuestoMinimo { get; set; }
        public decimal? PresupuestoMaximo { get; set; }
        public bool? State { get; set; }
    }
}
