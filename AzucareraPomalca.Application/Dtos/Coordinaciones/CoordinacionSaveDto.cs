namespace AzucareraPomalca.Application.Dtos.Coordinaciones
{
    public class CoordinacionSaveDto
    {
        public int? Id { get; set; }
        public int IdPuestoCoordinador { get; set; }
        public int IdPuestoCoordinado { get; set; }
    }
}
