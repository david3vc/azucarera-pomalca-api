using AzucareraPomalca.Application.Dtos.GradosAcademicos;
using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Application.Dtos.Puestos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.PuestosProfesiones
{
    public class PuestoProfesionDto
    {
        public int Id { get; set; }
        public int IdPuesto { get; set; }
        public int IdProfesion { get; set; }
        public int? IdGradoAcademico { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        //public PuestoDto Puesto { get; set; }
        public ProfesionDto Profesion { get; set; }
        public GradoAcademicoSimpleDto? GradoAcademico { get; set; }
    }
}
