using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Niveles.Profiles
{
    public class NivelProfile : Profile
    {
        public NivelProfile()
        {
            CreateMap<Nivel, NivelDto>();
            CreateMap<Nivel, NivelSimpleDto>();
        }
    }
}
