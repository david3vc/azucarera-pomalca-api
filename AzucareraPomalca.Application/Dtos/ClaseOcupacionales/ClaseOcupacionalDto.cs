using AzucareraPomalca.Application.Dtos.GrupoOcupacionales;

namespace AzucareraPomalca.Application.Dtos.ClaseOcupacionales
{
    public class ClaseOcupacionalDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
        public GrupoOcupacionalSimpleDto GrupoOcupacional { get; set; }
    }
}
