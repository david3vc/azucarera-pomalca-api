using AzucareraPomalca.Domain.Cores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Domain.Models
{
    public class PuestoProfesion : CoreModel<int>
    {
        public int IdPuesto { get; set; }
        public int IdProfesion { get; set; }
        public int? IdGradoAcademico { get; set; }

        public virtual Puesto Puesto { get; set; }
        public virtual Profesion Profesion { get; set; }
        public virtual GradoAcademico? GradoAcademico { get; set; }
    }
}
