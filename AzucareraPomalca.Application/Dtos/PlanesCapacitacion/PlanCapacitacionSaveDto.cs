namespace AzucareraPomalca.Application.Dtos.PlanesCapacitacion
{
    public class PlanCapacitacionSaveDto
    {
        public int Anio { get; set; }
        public string? Descripcion { get; set; }
        public decimal PresupuestoTotal { get; set; }
        public int FuerzaLaboral { get; set; }
    }
}
