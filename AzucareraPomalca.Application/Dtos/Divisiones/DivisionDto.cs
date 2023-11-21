using AzucareraPomalca.Application.Dtos.Gerencias;

namespace AzucareraPomalca.Application.Dtos.Divisiones
{
    public class DivisionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public GerenciaSimpleDto? Gerencia { get; set; }
    }
}
