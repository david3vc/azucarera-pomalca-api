namespace AzucareraPomalca.Application.Dtos.Roles
{
    public class RolSaveDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Consultar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
    }
}
