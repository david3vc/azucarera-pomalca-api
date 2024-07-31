namespace AzucareraPomalca.Domain.Models
{
    public class CursoDuroSugerido
    {
        public int? IdCurso { get; set; }
        public string? Codigo { get; set; }
        public string? Curso { get; set; }
        public string? TipoCurso { get; set; }
        public int? IdTipoCurso { get; set; }
        public string? Gerencia { get; set; }
        public int? EmpleadosNoCapacitados { get; set; }
    }
}
