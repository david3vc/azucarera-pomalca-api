namespace AzucareraPomalca.Application.Dtos.GrupoOcupacionales
{
    public class GrupoOcupacionalDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
    }
}
