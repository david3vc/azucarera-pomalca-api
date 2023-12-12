using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class PerfilCompetencia : CoreModel<int>
    {
        public int? IdGradoDominio { get; set; }
        public int IdPuesto { get; set; }

        public virtual GradoDominio? GradoDominio { get; set; }
        public virtual Puesto Puesto { get; set; }
    }
}
