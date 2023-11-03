using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.GrupoOcupacionales.Profiles
{
    public class GrupoOcupacionalProfile : Profile
    {
        public GrupoOcupacionalProfile()
        {
            CreateMap<GrupoOcupacional, GrupoOcupacionalDto>();
            CreateMap<GrupoOcupacional, GrupoOcupacionalSimpleDto>();
        }
    }
}
