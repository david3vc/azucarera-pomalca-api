using AutoMapper;
using AzucareraPomalca.Application.Dtos.PuestosProfesiones;
using AzucareraPomalca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.EmpleadoProfesiones.Profiles
{
    public class EmpleadoProfesionProfile : Profile
    {
        public EmpleadoProfesionProfile()
        {
            CreateMap<EmpleadoProfesion, EmpleadoProfesionDto>();
            CreateMap<EmpleadoProfesion, EmpleadoProfesionSaveDto>().ReverseMap();
        }
    }
}
