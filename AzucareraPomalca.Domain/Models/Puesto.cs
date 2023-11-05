using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Puesto : CoreModel<int>
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

        public virtual Puesto? PuestoSupervisor { get; set; }
        public virtual ClaseOcupacional? ClaseOcupacional { get; set; }
        public virtual Gerencia? Gerencia { get; set; }
        public virtual Division? Division { get; set; }
        public virtual Departamento? Departamento { get; set; }
        public virtual Seccion? Seccion { get; set; }
        public virtual ICollection<Puesto>? PuestosSubalternos { get; set; }
    }
}
