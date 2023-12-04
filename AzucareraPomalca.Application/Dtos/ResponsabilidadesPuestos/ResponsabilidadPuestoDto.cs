using AzucareraPomalca.Application.Dtos.Niveles;
using AzucareraPomalca.Application.Dtos.Responsabilidades;

namespace AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos
{
    public class ResponsabilidadPuestoDto
    {
        public int Id { get; set; }
        public int IdResponsabilidad { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public ResponsabilidadDto Responsabilidad { get; set; }
        public NivelDto? Nivel { get; set; }
    }
}
