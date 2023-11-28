namespace AzucareraPomalca.Application.Dtos.Usuarios
{
    public class UsuarioSaveDto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public int IdRol { get; set; }
    }
}
