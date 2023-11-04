using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Gerencia : CoreModel<int>
    {
        public string Nombre { get; set; }
    }
}
