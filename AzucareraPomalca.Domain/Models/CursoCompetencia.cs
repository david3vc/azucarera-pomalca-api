using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class CursoCompetencia : CoreModel<int>
    {
        public int IdCurso { get; set; }
        public int IdCompetencia { get; set; }
        public decimal Factor { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Competencia Competencia { get; set; }
    }
}
