using AutoMapper;
using AzucareraPomalca.Application.Dtos.FuncionesEspecificas;
using AzucareraPomalca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.PuestosProfesiones.Profiles
{
    public class PuestoProfesionProfile : Profile
    {
        public PuestoProfesionProfile()
        {
            CreateMap<PuestoProfesion, PuestoProfesionDto>();
            CreateMap<PuestoProfesion, PuestoProfesionSaveDto>().ReverseMap();
        }
    }
}
