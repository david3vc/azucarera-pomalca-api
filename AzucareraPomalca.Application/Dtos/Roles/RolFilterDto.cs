namespace AzucareraPomalca.Application.Dtos.Roles
{
    public class RolFilterDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool? State { get; set; }
        public bool? Consultar { get; set; }
        public bool? Editar { get; set; }
        public bool? Eliminar { get; set; }
    }
}
