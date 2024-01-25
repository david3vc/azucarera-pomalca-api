using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.Divisiones
{
    public class DivisionFilterDto
    {
        public string? Nombre { get; set; }
        public int? IdGerencia { get; set; }
        public bool? State { get; set; }
    }
}
