namespace AzucareraPomalca.Application.Dtos.CapacitacionesEmpleados
{
    public class CapacitacionEmpleadoSaveDto
    {
        public int? Id { get; set; }
        public int IdCapacitacion { get; set; }
        public int IdEmpleado { get; set; }
    }
}
