using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Departamento : CoreModel<int>
    {
        public string Nombre { get; set; }
        public int? IdDivision { get; set; }
        public int? IdGerencia { get; set; }

        public virtual Division? Division { get; set; }
        public virtual Gerencia? Gerencia { get; set; }
    }
}
