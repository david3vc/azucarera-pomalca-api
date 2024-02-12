using AzucareraPomalca.Domain.Cores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Domain.Models
{
    public class EmpleadoCurso : CoreModel<int>
    {
        public int IdCurso { get; set; }
        public int IdEmpleado { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
