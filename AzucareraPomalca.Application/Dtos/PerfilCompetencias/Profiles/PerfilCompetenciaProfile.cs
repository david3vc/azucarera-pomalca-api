using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.PerfilCompetencias.Profiles
{
    public class PerfilCompetenciaProfile : Profile
    {
        public PerfilCompetenciaProfile()
        {
            CreateMap<PerfilCompetencia, PerfilCompetenciaDto>();
            CreateMap<PerfilCompetencia, PerfilCompetenciaSaveDto>().ReverseMap();
            CreateMap<PerfilCompetencia, PerfilCompetenciaFilterDto>().ReverseMap();
            CreateMap<PagedResult<PerfilCompetencia>, PageResponse<PerfilCompetenciaDto>>();
        }
    }
}
