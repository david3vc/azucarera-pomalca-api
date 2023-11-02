namespace AzucareraPomalca.Application.Dtos.Profesiones
{
    public class ProfesionFilterDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool State { get; set; }
        public int IdTipoProfesion { get; set; }
    }
}
