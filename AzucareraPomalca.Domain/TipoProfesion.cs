using AzucareraPomalca.Domain.Core;

namespace AzucareraPomalca.Domain
{
    public class TipoProfesion : CoreModel<int>
    {
        public string Descripcion { get; set; }
    }
}
