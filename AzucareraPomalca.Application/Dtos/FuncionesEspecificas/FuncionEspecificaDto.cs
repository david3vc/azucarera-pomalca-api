namespace AzucareraPomalca.Application.Dtos.FuncionesEspecificas
{
    public class FuncionEspecificaDto
    {
        public int Id { get; set; }
        public int IdPuesto { get; set; }
        public string Descripcion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
    }
}
