using AzucareraPomalca.Application.Dtos.TipoEsfuerzoRequeridos;

namespace AzucareraPomalca.Application.Dtos.EsfuerzoRequeridos
{
    public class EsfuerzoRequeridoDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoEsfuerzoRequerido { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public TipoEsfuerzoRequeridoDto TipoEsfuerzoRequerido { get; set; }
    }
}
