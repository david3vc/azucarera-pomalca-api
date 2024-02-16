using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Application.Dtos.EmpleadoProfesiones;
using AzucareraPomalca.Application.Dtos.ExperienciaLaborales;
using AzucareraPomalca.Application.Dtos.PerfilCompetenciaEmpleados;

namespace AzucareraPomalca.Application.Dtos.Empleados
{
    public class EmpleadoSaveDto
    {
        public string Nombres { get; set; }
        public string Dni { get; set; }
        public string AppellidoPaterno { get; set; }
        public string AppellidoMaterno { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public int IdCondicionEmpleado { get; set; }
        public string? CodigoCardo { get; set; }
        public string? CodigoArea { get; set; }
        public int IdPuesto { get; set; }

        public List<EmpleadoProfesionSaveDto>? EmpleadoProfesionesSave { get; set; }
        public List<ExperienciaLaboralSaveDto>? ExperienciaLaboralesSave { get; set; }
        public List<EmpleadoCursoSaveDto>? EmpleadoCursosEspecificosSave { get; set; }
        public List<EmpleadoCursoSaveDto>? EmpleadoCursosHabilidadesBlandasSave { get; set; }
        public List<EmpleadoCursoSaveDto>? EmpleadoCursosSSOMMASave { get; set; }
        public List<EmpleadoCursoSaveDto>? EmpleadoCursosRSESave { get; set; }
        public List<PerfilCompetenciaEmpleadoSaveDto>? PerfilCompetenciaEmpleadosSave { get; set; }
    }
}
