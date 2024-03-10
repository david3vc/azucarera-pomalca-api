using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Application.Dtos.EmpleadoProfesiones;
using AzucareraPomalca.Application.Dtos.ExperienciaLaborales;
using AzucareraPomalca.Application.Dtos.PerfilCompetenciaEmpleados;

namespace AzucareraPomalca.Application.Dtos.Empleados
{
    public class EmpleadoSaveDto
    {
        public string Nombres { get; set; }
        public string NumeroDocumento { get; set; }
        public string AppellidoPaterno { get; set; }
        public string AppellidoMaterno { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public string? InicioPeriodoString { get; set; }
        public int IdCondicionEmpleado { get; set; }
        public int IdPuesto { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdSexo { get; set; }
        public int? IdTipoDocumentoIdentidad { get; set; }
        public DateTime? FinPeriodo { get; set; }
        public string? FinPeriodoString { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? FechaNacimientoString { get; set; }
        public string? Direccion { get; set; }
        public string? CodigoCargo { get; set; }
        public string? CodigoArea { get; set; }
        public int? IdClaseOcupacional { get; set; }

        public List<EmpleadoProfesionSaveDto>? EmpleadoProfesionesSave { get; set; }
        public List<ExperienciaLaboralSaveDto>? ExperienciaLaboralesSave { get; set; }
        public List<EmpleadoCursoSaveDto>? EmpleadoCursosEspecificosSave { get; set; }
        public List<EmpleadoCursoSaveDto>? EmpleadoCursosHabilidadesBlandasSave { get; set; }
        public List<EmpleadoCursoSaveDto>? EmpleadoCursosSSOMMASave { get; set; }
        public List<EmpleadoCursoSaveDto>? EmpleadoCursosRSESave { get; set; }
        public List<PerfilCompetenciaEmpleadoSaveDto>? PerfilCompetenciaEmpleadosSave { get; set; }
    }
}
