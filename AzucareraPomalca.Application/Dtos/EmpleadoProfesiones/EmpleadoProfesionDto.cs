using AzucareraPomalca.Application.Dtos.Empleados;
using AzucareraPomalca.Application.Dtos.GradosAcademicos;
using AzucareraPomalca.Application.Dtos.Profesiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.EmpleadoProfesiones
{
    public class EmpleadoProfesionDto
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public int IdProfesion { get; set; }
        public int? IdGradoAcademico { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }
        public EmpleadoDto Empleado { get; set; }
        public ProfesionDto Profesion { get; set; }
        public GradoAcademicoSimpleDto? GradoAcademico { get; set; }
    }
}
