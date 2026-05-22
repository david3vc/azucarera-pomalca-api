namespace AzucareraPomalca.Application.Dtos.Brechas
{
    /// <summary>
    /// Filtro para listar los empleados con brecha en UNA competencia blanda
    /// (la usa la auto-precarga de participantes al planear una capacitación).
    /// IdCompetencia es obligatorio; el alcance organizacional es opcional.
    /// </summary>
    public class BrechaBlandaEmpleadosFilterDto
    {
        public int IdCompetencia { get; set; }
        public int? IdGerencia { get; set; }
        public int? IdDivision { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdSeccion { get; set; }
        public int? IdPuesto { get; set; }
    }
}
