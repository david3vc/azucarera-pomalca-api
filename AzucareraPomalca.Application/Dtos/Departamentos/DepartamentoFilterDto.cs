namespace AzucareraPomalca.Application.Dtos.Departamentos
{
    public class DepartamentoFilterDto
    {
        public string? Nombre { get; set; }
        public int? IdDivision { get; set; }
        public int? IdGerencia { get; set; }
        public bool? State { get; set; }
    }
}
