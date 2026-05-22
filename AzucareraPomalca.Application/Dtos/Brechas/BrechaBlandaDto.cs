namespace AzucareraPomalca.Application.Dtos.Brechas
{
    /// <summary>
    /// Una competencia con brecha (la exige el puesto y el empleado no la alcanzó),
    /// con su demanda (nº de empleados) y los cursos candidatos que la cierran.
    /// </summary>
    public class BrechaBlandaDto
    {
        public int IdCompetencia { get; set; }
        public string? Codigo { get; set; }
        public string? Competencia { get; set; }
        public int NroEmpleados { get; set; }
        public List<CursoCandidatoDto> CursosCandidatos { get; set; } = new();
    }
}
