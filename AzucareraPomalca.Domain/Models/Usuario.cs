using AzucareraPomalca.Domain.Cores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Domain.Models
{
    public class Usuario : CoreModel<int>
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public int IdRol { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
