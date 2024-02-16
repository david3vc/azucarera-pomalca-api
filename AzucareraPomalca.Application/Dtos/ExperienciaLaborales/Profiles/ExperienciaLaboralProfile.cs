using AutoMapper;
using AzucareraPomalca.Domain.Models;

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
