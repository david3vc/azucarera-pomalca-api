using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class ResponsabilidadPuesto : CoreModel<int>
    {
        public int IdResponsabilidad { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }

        public virtual Responsabilidad Responsabilidad { get; set; }
        public virtual Puesto Puesto { get; set; }
        public virtual Nivel? Nivel { get; set; }
    }
}
