namespace AzucareraPomalca.Application.Dtos.ArbolOrganizacional
{
    public class NodoDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Nombre { get; set; }
        public string? TipoNodo { get; set; }
        public string? TipoUnidadOrganizacional { get; set; }
        public bool? EsJefe { get; set; }
        public List<NodoDto>? Children { get; set; }
    }
}
