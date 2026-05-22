namespace AzucareraPomalca.Application.Dtos.Brechas
{
    /// <summary>
    /// Curso que puede cerrar una competencia blanda (vínculo CursoCompetencia).
    /// </summary>
    public class CursoCandidatoDto
    {
        public int IdCurso { get; set; }
        public string? Codigo { get; set; }
        public string? Curso { get; set; }
        public string? TipoCurso { get; set; }
        public decimal Factor { get; set; }
    }
}
