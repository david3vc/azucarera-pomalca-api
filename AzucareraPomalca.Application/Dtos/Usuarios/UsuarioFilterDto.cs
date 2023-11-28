namespace AzucareraPomalca.Application.Dtos.Usuarios
{
    public class UsuarioFilterDto
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public int? IdRol { get; set; }
        public bool? State { get; set; }
    }
}
