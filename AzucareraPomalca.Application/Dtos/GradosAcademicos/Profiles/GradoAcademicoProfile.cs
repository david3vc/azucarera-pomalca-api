using AutoMapper;
using AzucareraPomalca.Domain.Models;

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