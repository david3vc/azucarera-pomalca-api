using AzucareraPomalca.Application.Dtos.TipoCursos;

namespace AzucareraPomalca.Application.Dtos.Cursos
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoCurso { get; set; }
        public string? Gerencia { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public TipoCursoDto TipoCurso { get; set; }
    }
}
