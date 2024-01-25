using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Gerencias.Profiles
{
    public class GerenciaProfile : Profile
    {
        public GerenciaProfile()
        {
            CreateMap<Gerencia, GerenciaDto>();
            CreateMap<Gerencia, GerenciaSimpleDto>();
            CreateMap<Gerencia, GerenciaFilterDto>().ReverseMap();
            CreateMap<PagedResult<Gerencia>, PageResponse<GerenciaDto>>();
        }
    }
}
