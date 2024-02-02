namespace AzucareraPomalca.Application.Dtos.Secciones
{
    public class SeccionFilterDto
    {
        public string? Nombre { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdDivision { get; set; }
        public int? IdGerencia { get; set; }
        public bool? State { get; set; }
    }
}
