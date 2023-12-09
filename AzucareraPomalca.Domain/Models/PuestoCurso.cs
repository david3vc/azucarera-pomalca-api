using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class PuestoCurso : CoreModel<int>
    {
        public int IdCurso { get; set; }
        public int IdPuesto { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Puesto Puesto { get; set; }
    }
}
