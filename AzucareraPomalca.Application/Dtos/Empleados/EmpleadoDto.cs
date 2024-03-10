using AzucareraPomalca.Application.Dtos.CondicionEmpleados;
using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Application.Dtos.EmpleadoProfesiones;
using AzucareraPomalca.Application.Dtos.ExperienciaLaborales;
using AzucareraPomalca.Application.Dtos.PerfilCompetenciaEmpleados;
using AzucareraPomalca.Application.Dtos.Puestos;
using AzucareraPomalca.Application.Dtos.TablaComunes;

namespace AzucareraPomalca.Application.Dtos.Empleados
{
    public class EmpleadoDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string? NumeroDocumento { get; set; }
        public string AppellidoPaterno { get; set; }
        public string AppellidoMaterno { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public int IdCondicionEmpleado { get; set; }
        public int IdPuesto { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdSexo { get; set; }
        public int? IdTipoDocumentoIdentidad { get; set; }
        public DateTime? FinPeriodo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public PuestoDto Puesto { get; set; }
        public CondicionEmpleadoDto CondicionEmpleado { get; set; }
        public TablaComunDto? EstadoCivil { get; set; }
        public TablaComunDto? Sexo { get; set; }
        public TablaComunDto? TipoDocumentoIdentidad { get; set; }

        public List<EmpleadoProfesionDto> EmpleadoProfesiones { get; set; }
        public List<ExperienciaLaboralDto> ExperienciaLaborales { get; set; }
        public List<EmpleadoCursoDto>? EmpleadoCursosEspecificos { get; set; }
        public List<EmpleadoCursoDto>? EmpleadoCursosHabilidadesBlandas { get; set; }
        public List<EmpleadoCursoDto>? EmpleadoCursosSSOMMA { get; set; }
        public List<EmpleadoCursoDto>? EmpleadoCursosRSE { get; set; }
        public List<PerfilCompetenciaEmpleadoDto> PerfilCompetenciaEmpleados { get; set; }
    }
}
