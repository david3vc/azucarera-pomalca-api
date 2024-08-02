namespace AzucareraPomalca.Application.Dtos.CapacitacionesEmpleados
{
    public class CapacitacionEmpleadoDto
    {
        public int Id { get; set; }
        public int IdCapacitacion { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
        public bool? Aprobado { get; set; }
    }
}
