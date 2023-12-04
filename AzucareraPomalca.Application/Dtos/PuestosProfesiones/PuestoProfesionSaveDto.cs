namespace AzucareraPomalca.Application.Dtos.PuestosProfesiones
{
    public class PuestoProfesionSaveDto
    {
        public int? Id { get; set; }
        public int IdPuesto { get; set; }
        public int IdProfesion { get; set; }
        public int? IdGradoAcademico { get; set; }
    }
}
