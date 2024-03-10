namespace AzucareraPomalca.Application.Dtos.TablaComunes
{
    public class TablaComunDto
    {
        public int Id { get; set; }
        public int IdTabla { get; set; }
        public int IdFila { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
    }
}
