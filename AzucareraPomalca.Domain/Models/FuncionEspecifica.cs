using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class FuncionEspecifica : CoreModel<int>
    {
        public string Descripcion { get; set; }
        public int IdPuesto { get; set; }

        public virtual Puesto Puesto { get; set; }
    }
}
