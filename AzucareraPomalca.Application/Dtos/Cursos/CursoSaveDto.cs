namespace AzucareraPomalca.Application.Dtos.Cursos
{
    public class CursoSaveDto
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoCurso { get; set; }
        public string? Gerencia { get; set; }
    }
}
