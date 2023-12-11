using AzucareraPomalca.Application.Dtos.EsfuerzoRequeridos;
using AzucareraPomalca.Application.Dtos.Niveles;

namespace AzucareraPomalca.Application.Dtos.EsfuerzoRequeridoPuestos
{
    public class EsfuerzoRequeridoPuestoDto
    {
        public int Id { get; set; }
        public int IdEsfuerzoRequerido { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public EsfuerzoRequeridoDto EsfuerzoRequerido { get; set; }
        public NivelDto Nivel { get; set; }
    }
}
