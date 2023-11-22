using AzucareraPomalca.Application.Dtos.Menus;
using AzucareraPomalca.Application.Dtos.Roles;

namespace AzucareraPomalca.Application.Dtos.Permisos
{
    public class PermisoDto
    {
        public int Id { get; set; }
        public bool Consultar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public int IdMenu { get; set; }
        public int IdRol { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public MenuDto Menu { get; set; }
        public RolDto Rol { get; set; }
    }
}
