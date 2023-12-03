using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.PuestosProfesiones
{
    public class PuestoProfesionSaveDto
    {
        public int? Id { get; set; }
        public int IdPuesto { get; set; }
        public int IdProfesion { get; set; }
        public int? IdGradoAcademico { get; set; }
    }
}
