namespace AzucareraPomalca.Application.Dtos.CondicionTrabajoPuestos
{
    public class CondicionTrabajoPuestoSaveDto
    {
        public int? Id { get; set; }
        public bool IsMarked { get; set; }
        public int IdCondicionTrabajo { get; set; }
        public int IdPuesto { get; set; }
    }
}
