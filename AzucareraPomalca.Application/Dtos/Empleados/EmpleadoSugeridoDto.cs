namespace AzucareraPomalca.Application.Dtos.Empleados
{
    public class EmpleadoSugeridoDto
    {
        public int? IdEmpleado { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Puesto { get; set; }
        public string? Gerencia { get; set; }
        public string? Division { get; set; }
        public string? Departamento { get; set; }
        public string? Seccion { get; set; }
        public int? CursosFaltantes { get; set; }
    }
}
