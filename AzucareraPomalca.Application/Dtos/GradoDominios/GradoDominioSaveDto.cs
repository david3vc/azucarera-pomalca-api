using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.GradoDominios
{
    public class GradoDominioSaveDto
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public int IdCompetencia { get; set; }
    }
}
