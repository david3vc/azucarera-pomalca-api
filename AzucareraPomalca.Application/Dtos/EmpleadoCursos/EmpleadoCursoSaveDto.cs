using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.EmpleadoCursos
{
    public class EmpleadoCursoSaveDto
    {
        public int? Id { get; set; }
        public int IdCurso { get; set; }
        public int IdEmpleado { get; set; }
    }
}
