using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Competencia : CoreModel<int>
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoCompetencia { get; set; }

        public virtual TipoCompetencia TipoCompetencia { get; set; }

        public virtual ICollection<GradoDominio> GradoDominios { get; set; }
        public virtual ICollection<CursoCompetencia>? CursoCompetencias { get; set; }
        public virtual ICollection<PerfilCompetenciaEmpleado>? PerfilCompetenciaEmpleados { get; set; }
    }
}
