using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class EsfuerzoRequeridoPuesto : CoreModel<int>
    {
        public int IdEsfuerzoRequerido { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }

        public virtual EsfuerzoRequerido EsfuerzoRequerido { get; set; }
        public virtual Puesto Puesto { get; set; }
        public virtual Nivel? Nivel { get; set; }
    }
}
