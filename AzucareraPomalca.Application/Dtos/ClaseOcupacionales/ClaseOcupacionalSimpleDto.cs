using AzucareraPomalca.Application.Dtos.GrupoOcupacionales;

namespace AzucareraPomalca.Application.Dtos.ClaseOcupacionales
{
    public class ClaseOcupacionalSimpleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public GrupoOcupacionalSimpleDto GrupoOcupacional { get; set; }
    }
}
