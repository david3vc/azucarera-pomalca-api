using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.ExperienciaLaborales
{
    public class ExperienciaLaboralSaveDto
    {
        public int? Id { get; set; }
        public string Empresa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string TipoExperiencia { get; set; }
        public int IdEmpleado { get; set; }
    }
}
