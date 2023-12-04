namespace AzucareraPomalca.Application.Dtos.Responsabilidades
{
    public class ResponsabilidadDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
    }
}
