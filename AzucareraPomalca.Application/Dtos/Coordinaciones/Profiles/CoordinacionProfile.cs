using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Coordinaciones.Profiles
{
    public class CoordinacionProfile : Profile
    {
        public CoordinacionProfile()
        {
            CreateMap<Coordinacion, CoordinacionDto>();
            CreateMap<Coordinacion, CoordinacionSaveDto>().ReverseMap();
        }
    }
}
