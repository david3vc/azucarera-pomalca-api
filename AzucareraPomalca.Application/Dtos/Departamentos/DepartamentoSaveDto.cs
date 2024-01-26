using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.Departamentos
{
    public class DepartamentoSaveDto
    {
        public string Nombre { get; set; }
        public int? IdDivision { get; set; }
        public int? IdGerencia { get; set; }
    }
}
