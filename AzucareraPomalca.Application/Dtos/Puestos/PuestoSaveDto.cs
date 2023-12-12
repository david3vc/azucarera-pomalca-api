using AzucareraPomalca.Application.Dtos.CondicionTrabajoPuestos;
using AzucareraPomalca.Application.Dtos.Coordinaciones;
using AzucareraPomalca.Application.Dtos.EsfuerzoRequeridoPuestos;
using AzucareraPomalca.Application.Dtos.FuncionesEspecificas;
using AzucareraPomalca.Application.Dtos.Misiones;
using AzucareraPomalca.Application.Dtos.PerfilCompetencias;
using AzucareraPomalca.Application.Dtos.PuestosCursos;
using AzucareraPomalca.Application.Dtos.PuestosProfesiones;
using AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos;
using AzucareraPomalca.Application.Dtos.TomaDecisionPuestos;

namespace AzucareraPomalca.Application.Dtos.Puestos
{
    public class PuestoSaveDto
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? AmbienteTrabajo { get; set; }
        public int? ExperienciaGeneralMinima { get; set; }
        public int? ExperienciaGeneralPreferencia { get; set; }
        public int? ExperienciaPuestoMinima { get; set; }
        public int? ExperienciaPuestoPreferencia { get; set; }
        public int? IdPuestoSupervisor { get; set; }
        public int? IdClaseOcupacional { get; set; }
        public int? IdGerencia { get; set; }
        public int? IdDivision { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdSeccion { get; set; }

        public List<MisionSaveDto>? MisionesSave { get; set; }
        public List<FuncionEspecificaSaveDto>? FuncionesEspecificasSave { get; set; }
        public List<CoordinacionSaveDto>? CoordinacionesMismaGerenciaSave { get; set; }
        public List<CoordinacionSaveDto>? CoordinacionesOtraGerenciaSave { get; set; }
        public List<CoordinacionSaveDto>? CoordinacionesExternasSave { get; set; }
        public List<PuestoProfesionSaveDto>? PuestosProfesionesSave { get; set; }
        public List<ResponsabilidadPuestoSaveDto>? ResponsabilidadesPuestosSave { get; set; }
        public List<PuestoCursoSaveDto>? PuestosCursosEspecificosSave { get; set; }
        public List<PuestoCursoSaveDto>? PuestosCursosHabilidadesBlandasSave { get; set; }
        public List<PuestoCursoSaveDto>? PuestosCursosSSOMMASave { get; set; }
        public List<PuestoCursoSaveDto>? PuestosCursosRSESave { get; set; }
        public List<CondicionTrabajoPuestoSaveDto>? CondicionTrabajoPuestosSave { get; set; }
        public List<TomaDecisionPuestoSaveDto>? TomaDecisionPuestosSave { get; set; }
        public List<EsfuerzoRequeridoPuestoSaveDto>? EsfuerzoRequeridoPuestosSave { get; set; }
        public List<PerfilCompetenciaSaveDto>? PerfilCompetenciasSave { get; set; }
    }
}
