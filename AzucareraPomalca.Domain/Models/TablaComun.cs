using AzucareraPomalca.Domain.Cores.Models;

namespace AzucareraPomalca.Domain.Models
{
    public class TablaComun : CoreModel<int>
    {
        public int IdTabla { get; set; }
        public int IdFila { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Empleado> EmpleadosEstadoCivil { get; set; }
        public virtual ICollection<Empleado> EmpleadosSexo { get; set; }
        public virtual ICollection<Empleado> EmpleadosTipoDocumentoIdentidad { get; set; }
        public virtual ICollection<Capacitacion> CapacitacionesTipoFacilitador { get; set; }
        public virtual ICollection<Capacitacion> CapacitacionesModalidad { get; set; }
    }
}
