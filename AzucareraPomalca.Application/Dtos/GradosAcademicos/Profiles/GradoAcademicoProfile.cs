using AutoMapper;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Dtos.GradosAcademicos.Profiles
{
    public class GradoAcademicoProfile : Profile
    {
        public GradoAcademicoProfile()
        {
            CreateMap<GradoAcademico, GradoAcademicoDto>();
            CreateMap<GradoAcademico, GradoAcademicoSimpleDto>();
        }
    }
}