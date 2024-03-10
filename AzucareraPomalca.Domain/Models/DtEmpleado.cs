namespace AzucareraPomalca.Domain.Models
{
    public class DtEmpleado
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
        public string Nombres { get; set; }
        public string? NumeroDocumento { get; set; }
        public string AppellidoPaterno { get; set; }
        public string AppellidoMaterno { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public string? InicioPeriodoString { get; set; }
        public int IdCondicionEmpleado { get; set; }
        public int IdPuesto { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdSexo { get; set; }
        public int? IdTipoDocumentoIdentidad { get; set; }
        public DateTime? FinPeriodo { get; set; }
        public string? FinPeriodoString { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? FechaNacimientoString { get; set; }
        public string? Direccion { get; set; }
        public string? CodigoArea { get; set; }
        public string? CodigoCargo { get; set; }
    }
}
