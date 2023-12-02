using AzucareraPomalca.Application.Dtos.Puestos;

namespace AzucareraPomalca.Application.Dtos.Coordinaciones
{
    public class CoordinacionDto
    {
        public int Id { get; set; }
        public int IdPuestoCoordinador { get; set; }
        public int IdPuestoCoordinado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
        public PuestoDto? PuestoCoordinado { get; set; }
    }
}
