namespace AzucareraPomalca.Application.Dtos.Capacitaciones
{
    public class CapacitacionFilterDto
    {
        public string? Descripcion { get; set; }
        public int? NumeroHoras { get; set; }
        public int? NumeroParticipantes { get; set; }
        public int? HorasHombre { get; set; }
        public decimal? Costo { get; set; }
        public decimal? CostoXTrabjador { get; set; }
        public decimal? CostoXHorasHombre { get; set; }
        public int? IdCurso { get; set; }
        public int? IdTipoFacilitador { get; set; }
        public int? IdModalidad { get; set; }
    }
}
