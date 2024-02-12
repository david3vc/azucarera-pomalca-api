using AzucareraPomalca.Application.Dtos.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.EmpleadoCursos
{
    public class EmpleadoCursoDto
    {
        public int Id { get; set; }
        public int IdCurso { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public CursoDto Curso { get; set; }
    }
}
