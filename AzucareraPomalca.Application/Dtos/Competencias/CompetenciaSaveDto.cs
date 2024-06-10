using AzucareraPomalca.Application.Dtos.GradoDominios;

namespace AzucareraPomalca.Application.Dtos.Competencias
{
    public class CompetenciaSaveDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoCompetencia { get; set; }
        public List<GradoDominioSaveDto> GradosDominioSave { get; set; }
    }
}
