using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Menu : CoreModel<int>
    {
        public string Nombre { get; set; }
        public int? Orden { get; set; }
        public int? Nivel { get; set; }
        public string? Icono { get; set; }
        public string? UrlMenu { get; set; }
        public bool? Visible { get; set; }

        public virtual ICollection<Permiso> Permisos { get; set; }
    }
}
