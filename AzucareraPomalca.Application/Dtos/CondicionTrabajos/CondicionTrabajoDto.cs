using AzucareraPomalca.Application.Dtos.TipoCondicionTrabajos;

namespace AzucareraPomalca.Application.Dtos.CondicionTrabajos
{
    public class CondicionTrabajoDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoCondicionTrabajo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public TipoCondicionTrabajoDto TipoCondicionTrabajo { get; set; }
    }
}
