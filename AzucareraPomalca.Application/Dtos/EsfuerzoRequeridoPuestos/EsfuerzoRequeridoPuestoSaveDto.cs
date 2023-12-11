namespace AzucareraPomalca.Application.Dtos.EsfuerzoRequeridoPuestos
{
    public class EsfuerzoRequeridoPuestoSaveDto
    {
        public int? Id { get; set; }
        public int IdEsfuerzoRequerido { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }
    }
}
