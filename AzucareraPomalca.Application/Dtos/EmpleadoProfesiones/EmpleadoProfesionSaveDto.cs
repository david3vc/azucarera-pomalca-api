namespace AzucareraPomalca.Application.Dtos.EmpleadoProfesiones
{
    public class EmpleadoProfesionSaveDto
    {
        public int? Id { get; set; }
        public int IdEmpleado { get; set; }
        public int IdProfesion { get; set; }
        public int? IdGradoAcademico { get; set; }
    }
}
