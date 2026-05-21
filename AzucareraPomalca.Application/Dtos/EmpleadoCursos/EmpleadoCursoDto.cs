using AzucareraPomalca.Application.Dtos.Cursos;

namespace AzucareraPomalca.Application.Dtos.EmpleadoCursos
{
    public class EmpleadoCursoDto
    {
        public int Id { get; set; }
        public int IdCurso { get; set; }
        public int IdEmpleado { get; set; }
        public decimal HorasAcumuladas { get; set; }
        public DateTime? FechaCalculo { get; set; }
        public int? HorasRequeridas { get; set; }
        public decimal Brecha { get; set; }
        public decimal Sobrante { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public CursoDto Curso { get; set; }
    }
}
