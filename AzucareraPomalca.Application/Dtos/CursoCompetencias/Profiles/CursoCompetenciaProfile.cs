using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.CursoCompetencias.Profiles
{
    public class CursoCompetenciaProfile : Profile
    {
        public CursoCompetenciaProfile()
        {
            CreateMap<CursoCompetencia, CursoCompetenciaDto>();
            CreateMap<CursoCompetencia, CursoCompetenciaSaveDto>().ReverseMap();
            CreateMap<CursoCompetencia, CursoCompetenciaFilterDto>().ReverseMap();

            CreateMap<PagedResult<CursoCompetencia>, PageResponse<CursoCompetenciaDto>>();
        }
    }
}
