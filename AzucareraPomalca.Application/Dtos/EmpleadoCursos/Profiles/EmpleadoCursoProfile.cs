using AutoMapper;
using AzucareraPomalca.Application.Dtos.PuestosCursos;
using AzucareraPomalca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.EmpleadoCursos.Profiles
{
    public class EmpleadoCursoProfile : Profile
    {
        public EmpleadoCursoProfile()
        {
            CreateMap<EmpleadoCurso, EmpleadoCursoDto>();
            CreateMap<EmpleadoCurso, EmpleadoCursoSaveDto>().ReverseMap();
        }
    }
}
