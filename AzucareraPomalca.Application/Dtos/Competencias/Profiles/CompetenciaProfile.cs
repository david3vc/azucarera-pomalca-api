using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Competencias.Profiles
{
    public class CompetenciaProfile : Profile
    {
        public CompetenciaProfile()
        {
            CreateMap<Competencia, CompetenciaDto>();
            CreateMap<Competencia, CompetenciaSimpleDto>();
            CreateMap<Competencia, CompetenciaSaveDto>().ReverseMap();
            CreateMap<Competencia, CompetenciaFilterDto>().ReverseMap();

            CreateMap<PagedResult<Competencia>, PageResponse<CompetenciaDto>>();
        }
    }
}
