using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Gerencias.Profiles
{
    public class GerenciaProfile : Profile
    {
        public GerenciaProfile()
        {
            CreateMap<Gerencia, GerenciaDto>();
            CreateMap<Gerencia, GerenciaSimpleDto>();
        }
    }
}
