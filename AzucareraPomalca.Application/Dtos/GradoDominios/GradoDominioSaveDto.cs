namespace AzucareraPomalca.Application.Dtos.GradoDominios
{
    public class GradoDominioSaveDto
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public int IdCompetencia { get; set; }
    }
}
