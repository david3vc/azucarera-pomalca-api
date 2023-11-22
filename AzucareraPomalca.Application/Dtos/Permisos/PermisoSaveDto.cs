namespace AzucareraPomalca.Application.Dtos.Permisos
{
    public class PermisoSaveDto
    {
        public bool Consultar { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }
        public int IdMenu { get; set; }
        public int IdRol { get; set; }
    }
}
