using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Division : CoreModel<int>
    {
        public string Nombre { get; set; }
        public int? IdGerencia { get; set; }

        public virtual Gerencia? Gerencia { get; set; }
    }
}
