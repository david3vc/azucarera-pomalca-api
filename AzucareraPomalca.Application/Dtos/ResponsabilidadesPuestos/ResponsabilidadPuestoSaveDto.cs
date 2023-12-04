namespace AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos
{
    public class ResponsabilidadPuestoSaveDto
    {
        public int? Id { get; set; }
        public int IdResponsabilidad { get; set; }
        public int IdPuesto { get; set; }
        public int? IdNivel { get; set; }
    }
}
