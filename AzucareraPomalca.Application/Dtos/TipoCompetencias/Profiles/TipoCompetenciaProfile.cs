using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.TipoCompetencias.Profiles
{
    public class TipoCompetenciaProfile : Profile
    {
        public TipoCompetenciaProfile()
        {
            CreateMap<TipoCompetencia, TipoCompetenciaDto>();
        }
    }
}
