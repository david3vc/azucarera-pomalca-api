using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.TipoEsfuerzoRequeridos.Profiles
{
    public class TipoEsfuerzoRequeridoProfile : Profile
    {
        public TipoEsfuerzoRequeridoProfile()
        {
            CreateMap<TipoEsfuerzoRequerido, TipoEsfuerzoRequeridoDto>();
        }
    }
}
