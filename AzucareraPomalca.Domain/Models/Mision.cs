using AzucareraPomalca.Domain.Cores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Domain.Models
{
    public class Mision : CoreModel<int>
    {
        public string Descripcion { get; set; }
        public int IdPuesto { get; set; }

        public virtual Puesto Puesto { get; set; }
    }
}
