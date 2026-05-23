using AzucareraPomalca.Application.Dtos.CapacitacionesEmpleados;
using AzucareraPomalca.Application.Dtos.Cursos;
using AzucareraPomalca.Application.Dtos.TablaComunes;

namespace AzucareraPomalca.Application.Dtos.Capacitaciones
{
    public class CapacitacionDto
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public int NumeroHoras { get; set; }
        public int NumeroParticipantes { get; set; }
        public int HorasHombre { get; set; }
        public decimal Costo { get; set; }
        public decimal CostoXTrabjador { get; set; }
        public decimal CostoXHorasHombre { get; set; }
        public int IdCurso { get; set; }
        public int? IdTipoFacilitador { get; set; }
        public int? IdModalidad { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
        public bool Evaluado { get; set; }
        public string? Profesor { get; set; }
        public DateTime? FechaInicio { get; set; }
        public int? IdPlanCapacitacion { get; set; }
        public int? IdCompetencia { get; set; }

        public CursoDto Curso { get; set; }
        public TablaComunDto? TipoFacilitador { get; set; }
        public TablaComunDto? Modalidad { get; set; }
        public TablaComunDto? EstadoPlan { get; set; }
        public List<CapacitacionEmpleadoDto> CapacitacionEmpleados { get; set; }
    }
}
