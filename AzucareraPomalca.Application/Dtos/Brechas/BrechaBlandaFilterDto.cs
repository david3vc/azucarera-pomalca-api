namespace AzucareraPomalca.Application.Dtos.Brechas
{
    /// <summary>
    /// Alcance organizacional opcional para el diagnóstico de brechas blandas
    /// (toda la empresa si todo es null, o acotado a gerencia/área).
    /// </summary>
    public class BrechaBlandaFilterDto
    {
        public int? IdGerencia { get; set; }
        public int? IdDivision { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdSeccion { get; set; }
        public int? IdPuesto { get; set; }
    }
}
