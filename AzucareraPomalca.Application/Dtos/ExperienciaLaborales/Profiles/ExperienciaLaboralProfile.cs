using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Empleados;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.ExperienciaLaborales.Profiles
{
    public class ExperienciaLaboralProfile : Profile
    {
        public ExperienciaLaboralProfile()
        {
            CreateMap<ExperienciaLaboral, ExperienciaLaboralDto>();
            CreateMap<ExperienciaLaboral, ExperienciaLaboralSaveDto>().ReverseMap();
        }
    }
}
