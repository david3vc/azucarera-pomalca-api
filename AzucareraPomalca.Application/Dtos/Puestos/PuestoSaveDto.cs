using AzucareraPomalca.Application.Dtos.Misiones;

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
    }
}
