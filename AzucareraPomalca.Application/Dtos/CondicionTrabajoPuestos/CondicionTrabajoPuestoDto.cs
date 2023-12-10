using AzucareraPomalca.Application.Dtos.CondicionTrabajos;

namespace AzucareraPomalca.Application.Dtos.CondicionTrabajoPuestos
{
    public class CondicionTrabajoPuestoDto
    {
        public int Id { get; set; }
        public bool IsMarked { get; set; }
        public int IdCondicionTrabajo { get; set; }
        public int IdPuesto { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public CondicionTrabajoDto CondicionTrabajo { get; set; }
    }
}
