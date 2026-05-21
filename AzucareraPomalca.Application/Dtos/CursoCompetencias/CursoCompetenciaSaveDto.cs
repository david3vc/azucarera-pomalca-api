namespace AzucareraPomalca.Application.Dtos.CursoCompetencias
{
    public class CursoCompetenciaSaveDto
    {
        public int? Id { get; set; }
        public int IdCurso { get; set; }
        public int IdCompetencia { get; set; }
        public decimal Factor { get; set; }
    }
}
