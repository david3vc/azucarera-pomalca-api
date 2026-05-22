namespace AzucareraPomalca.Application.Dtos.PlanesCapacitacion
{
    /// <summary>
    /// Cuadros (KPIs) del plan. NO se persisten: se calculan al vuelo desde las
    /// líneas y sus participantes reales (DISEÑO v2 §0.10/§5.3).
    /// </summary>
    public class PlanResumenDto
    {
        public decimal CostoTotal { get; set; }
        public int HorasHombreTotal { get; set; }
        public int NroCursos { get; set; }
        public decimal PresupuestoTotal { get; set; }
        public decimal SaldoPresupuesto { get; set; }
        public int FuerzaLaboral { get; set; }
        public decimal HhPorTrabajador { get; set; }
        public decimal CostoPorHh { get; set; }

        public List<ResumenItemDto> PorPrograma { get; set; } = new();
        public List<ResumenItemDto> PorGerencia { get; set; } = new();
        public List<ResumenItemDto> PorNivel { get; set; } = new();
        public List<ResumenItemDto> PorModalidad { get; set; } = new();
        public List<ResumenItemDto> PorFacilitador { get; set; } = new();
    }

    public class ResumenItemDto
    {
        public string Etiqueta { get; set; } = string.Empty;
        public int NroCursos { get; set; }
        public int NroParticipantes { get; set; }
        public int HorasHombre { get; set; }
        public decimal Costo { get; set; }
        public decimal PorcentajePresupuesto { get; set; }
    }
}
