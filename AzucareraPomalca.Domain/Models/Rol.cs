using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Rol : CoreModel<int>
    {
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
