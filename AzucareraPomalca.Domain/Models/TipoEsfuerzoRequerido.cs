using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TipoEsfuerzoRequerido : CoreModel<int>
    {
        public string Descripcion { get; set; }

        public virtual ICollection<EsfuerzoRequerido> EsfuerzoRequeridos { get; set; }
    }
}
