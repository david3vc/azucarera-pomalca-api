using AutoMapper;
using AzucareraPomalca.Domain.Models;

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
