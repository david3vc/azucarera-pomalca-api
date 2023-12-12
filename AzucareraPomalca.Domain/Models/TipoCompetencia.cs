using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TipoCompetencia : CoreModel<int>
    {
        public string Descripcion { get; set; }

        public virtual ICollection<Competencia> Competencias { get; set; }
    }
}
