using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class GradoDominio : CoreModel<int>
    {
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public int HorasRequeridas { get; set; }
        public int IdCompetencia { get; set; }

        public virtual Competencia CompetenciaSimple { get; set; }
        public virtual ICollection<PerfilCompetencia>? PerfilCompetencias { get; set; }
        public virtual ICollection<PerfilCompetenciaEmpleado>? PerfilCompetenciaEmpleados { get; set; }
    }
}
