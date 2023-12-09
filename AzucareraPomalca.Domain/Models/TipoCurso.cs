using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TipoCurso : CoreModel<int>
    {
        public string Descripcion { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }
    }
}
