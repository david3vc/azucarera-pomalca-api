namespace AzucareraPomalca.Application.Dtos.PuestosCursos
{
    public class PuestoCursoSaveDto
    {
        public int? Id { get; set; }
        public int IdCurso { get; set; }
        public int IdPuesto { get; set; }
        public int HorasRequeridas { get; set; }
    }
}
