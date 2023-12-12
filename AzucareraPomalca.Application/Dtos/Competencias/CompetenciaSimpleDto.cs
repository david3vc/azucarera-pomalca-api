using AzucareraPomalca.Application.Dtos.TipoCompetencias;

namespace AzucareraPomalca.Application.Dtos.Competencias
{
    public class CompetenciaSimpleDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoCompetencia { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public TipoCompetenciaDto TipoCompetencia { get; set; }
    }
}
