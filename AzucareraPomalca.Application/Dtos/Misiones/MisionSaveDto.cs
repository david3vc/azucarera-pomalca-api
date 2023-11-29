using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.Misiones
{
    public class MisionSaveDto
    {
        public int? Id { get; set; }
        public int IdPuesto { get; set; }
        public string Descripcion { get; set; }
    }
}
