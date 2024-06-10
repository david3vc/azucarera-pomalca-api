namespace AzucareraPomalca.Application.Dtos.Competencias
{
    public class CompetenciaFilterDto
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? IdTipoCompetencia { get; set; }
        public bool? State { get; set; }
    }
}
