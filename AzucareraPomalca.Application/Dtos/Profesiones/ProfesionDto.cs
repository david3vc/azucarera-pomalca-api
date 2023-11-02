using AzucareraPomalca.Application.Dtos.TipoProfesiones;

namespace AzucareraPomalca.Application.Dtos.Profesiones
{
    public class ProfesionDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
        public TipoProfesionSimpleDto TipoProfesion { get; set; }
    }
}
