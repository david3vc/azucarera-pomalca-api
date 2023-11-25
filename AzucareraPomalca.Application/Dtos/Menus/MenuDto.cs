namespace AzucareraPomalca.Application.Dtos.Menus
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Orden { get; set; }
        public int? Nivel { get; set; }
        public string? Icono { get; set; }
        public string? UrlMenu { get; set; }
        public bool? Visible { get; set; }
        public string? Descripcion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
        public int? IdMenuPadre { get; set; }

        public MenuDto? MenuPadre { get; set; }
    }
}
