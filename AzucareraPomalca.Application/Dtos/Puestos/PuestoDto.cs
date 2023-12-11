using AzucareraPomalca.Application.Dtos.ClaseOcupacionales;
using AzucareraPomalca.Application.Dtos.CondicionTrabajoPuestos;
using AzucareraPomalca.Application.Dtos.Coordinaciones;
using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Application.Dtos.FuncionesEspecificas;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Application.Dtos.Misiones;
using AzucareraPomalca.Application.Dtos.PuestosCursos;
using AzucareraPomalca.Application.Dtos.PuestosProfesiones;
using AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos;
using AzucareraPomalca.Application.Dtos.Secciones;
using AzucareraPomalca.Application.Dtos.TomaDecisionPuestos;

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
        public GerenciaSimpleDto? Gerencia { get; set; }
        public DivisionSimpleDto? Division { get; set; }
        public DepartamentoSimpleDto? Departamento { get; set; }
        public SeccionSimpleDto? Seccion { get; set; }

        public List<MisionDto> Misiones { get; set; }
        public List<FuncionEspecificaDto> FuncionesEspecificas { get; set; }
        public List<CoordinacionDto> CoordinacionesMismaGerencia { get; set; }
        public List<CoordinacionDto> CoordinacionesOtraGerencia { get; set; }
        public List<CoordinacionDto> CoordinacionesExternas { get; set; }
        public List<PuestoProfesionDto> PuestosProfesiones { get; set; }
        public List<ResponsabilidadPuestoDto> ResponsabilidadesPuestos { get; set; }
        public List<PuestoCursoDto> PuestosCursosEspecificos { get; set; }
        public List<PuestoCursoDto> PuestosCursosHabilidadesBlandas { get; set; }
        public List<PuestoCursoDto> PuestosCursosSSOMMA { get; set; }
        public List<PuestoCursoDto> PuestosCursosRSE { get; set; }
        public List<CondicionTrabajoPuestoDto> CondicionTrabajoPuestos { get; set; }
        public List<TomaDecisionPuestoDto> TomaDecisionPuestos { get; set; }
    }
}
