namespace AzucareraPomalca.Domain.Models
{
    public class EmpleadoSugerido
    {
        public int? IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int? IdPuesto { get; set; }
        public string? Puesto { get; set; }
        public int? IdGerencia { get; set; }
        public string? Gerencia { get; set; }
        public int? IdDivision { get; set; }
        public string? Division { get; set; }
        public int? IdDepartamento { get; set; }
        public string? Departamento { get; set; }
        public int? IdSeccion { get; set; }
        public string? Seccion { get; set; }
        public int? CursosFaltantes { get; set; }
    }
}
