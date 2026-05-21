using AzucareraPomalca.Application.Dtos.Competencias;
using AzucareraPomalca.Application.Dtos.Cursos;

namespace AzucareraPomalca.Application.Dtos.CursoCompetencias
{
    public class CursoCompetenciaDto
    {
        public int Id { get; set; }
        public int IdCurso { get; set; }
        public int IdCompetencia { get; set; }
        public decimal Factor { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public CursoDto? Curso { get; set; }
        public CompetenciaSimpleDto? Competencia { get; set; }
    }
}
