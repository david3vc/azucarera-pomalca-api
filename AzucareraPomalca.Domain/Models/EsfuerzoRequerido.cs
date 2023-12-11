using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class EsfuerzoRequerido : CoreModel<int>
    {
        public string Descripcion { get; set; }
        public int IdTipoEsfuerzoRequerido { get; set; }

        public virtual TipoEsfuerzoRequerido TipoEsfuerzoRequerido { get; set; }
        public virtual ICollection<EsfuerzoRequeridoPuesto> EsfuerzoRequeridoPuestos { get; set; }
    }
}
