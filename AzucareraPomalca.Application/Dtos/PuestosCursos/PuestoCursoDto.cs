using AzucareraPomalca.Application.Dtos.Cursos;

namespace AzucareraPomalca.Application.Dtos.PuestosCursos
{
    public class PuestoCursoDto
    {
        public int Id { get; set; }
        public int IdCurso { get; set; }
        public int IdPuesto { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public CursoDto Curso { get; set; }
    }
}
