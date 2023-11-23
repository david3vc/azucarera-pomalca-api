using AzucareraPomalca.Core.Securities.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.Usuarios
{
    public class UsuarioSecurityDto : UsuarioDto
    {
        public SecurityEntity? Security { get; set; }
    }
}
