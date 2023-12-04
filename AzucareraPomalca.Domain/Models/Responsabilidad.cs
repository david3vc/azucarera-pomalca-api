using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Responsabilidad : CoreModel<int>
    {
        public string Descripcion { get; set; }

        public virtual ICollection<ResponsabilidadPuesto> ResponsabilidadesPuestos { get; set; }
    }
}
