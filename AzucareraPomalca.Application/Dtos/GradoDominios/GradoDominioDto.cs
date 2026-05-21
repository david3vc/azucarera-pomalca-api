using AzucareraPomalca.Application.Dtos.Competencias;

namespace AzucareraPomalca.Application.Dtos.GradoDominios
{
    public class GradoDominioDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Nivel { get; set; }
        public int HorasRequeridas { get; set; }
        public int IdCompetencia { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public CompetenciaSimpleDto CompetenciaSimple { get; set; }
    }
}
