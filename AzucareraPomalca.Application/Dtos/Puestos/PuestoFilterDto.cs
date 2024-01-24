namespace AzucareraPomalca.Application.Dtos.Puestos
{
    public class PuestoFilterDto
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int? IdGerencia { get; set; }
        public int? IdClaseOcupacional { get; set; }
        public bool? State { get; set; }
    }
}
