namespace AzucareraPomalca.Application.Dtos.Cursos
{
    public class CursoFilterDto
    {
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? IdTipoCurso { get; set; }
        public string? Gerencia { get; set; }
        public bool? State { get; set; }
    }
}
