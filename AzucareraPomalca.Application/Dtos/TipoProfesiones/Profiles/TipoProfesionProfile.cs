using AutoMapper;
using AzucareraPomalca.Domain;

namespace AzucareraPomalca.Application.Dtos.TipoProfesiones.Profiles
{
    public class TipoProfesionProfile : Profile
    {
        public TipoProfesionProfile()
        {
            CreateMap<TipoProfesion, TipoProfesionDto>();
            CreateMap<TipoProfesion, TipoProfesionSimpleDto>();
        }
    }
}
