using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.EsfuerzoRequeridos.Profiles
{
    public class EsfuerzoRequeridoProfile : Profile
    {
        public EsfuerzoRequeridoProfile()
        {
            CreateMap<EsfuerzoRequerido, EsfuerzoRequeridoDto>();
        }
    }
}
