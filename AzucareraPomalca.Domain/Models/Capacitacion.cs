using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class Capacitacion : CoreModel<int>
    {
        public string? Descripcion { get; set; }
        public int NumeroHoras { get; set; }
        public int NumeroParticipantes { get; set; }
        public int HorasHombre { get; set; }
        public decimal Costo { get; set; }
        public decimal CostoXTrabjador { get; set; }
        public decimal CostoXHorasHombre { get; set; }
        public int IdCurso { get; set; }
        public int IdTipoFacilitador { get; set; }
        public int IdModalidad { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual TablaComun TipoFacilitador { get; set; }
        public virtual TablaComun Modalidad { get; set; }

        public virtual ICollection<CapacitacionEmpleado> CapacitacionEmpleados { get; set; }
    }
}
