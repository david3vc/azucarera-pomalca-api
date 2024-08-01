using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Curso : CoreModel<int>
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoCurso { get; set; }
        public string? Gerencia { get; set; }

        public virtual TipoCurso TipoCurso { get; set; }

        public virtual ICollection<PuestoCurso> PuestosCursos { get; set; }
        public virtual ICollection<EmpleadoCurso> EmpleadoCursos { get; set; }
        public virtual ICollection<Capacitacion> Capacitaciones { get; set; }
    }
}
