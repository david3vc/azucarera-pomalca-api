using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.PerfilCompetencias.Profiles
{
    public class PerfilCompetenciaProfile : Profile
    {
        public PerfilCompetenciaProfile()
        {
            CreateMap<PerfilCompetencia, PerfilCompetenciaDto>();
            CreateMap<PerfilCompetencia, PerfilCompetenciaSaveDto>().ReverseMap();
        }
    }
}
