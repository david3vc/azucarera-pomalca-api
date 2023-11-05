using AzucareraPomalca.Application.Dtos.ClaseOcupacionales;
using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Application.Dtos.Secciones;

namespace AzucareraPomalca.Application.Dtos.Puestos
{
    public class PuestoDto
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? AmbienteTrabajo { get; set; }
        public int? ExperienciaGeneralMinima { get; set; }
        public int? ExperienciaGeneralPreferencia { get; set; }
        public int? ExperienciaPuestoMinima { get; set; }
        public int? ExperienciaPuestoPreferencia { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public PuestoDto? PuestoSupervisor { get; set; }
        public ClaseOcupacionalDto? ClaseOcupacional { get; set; }
        public GerenciaDto? Gerencia { get; set; }
        public DivisionDto? Division { get; set; }
        public DepartamentoDto? Departamento { get; set; }
        public SeccionDto? Seccion { get; set; }
        public List<PuestoDto>? PuestosSubalternos { get; set; }
    }
}
