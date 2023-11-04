using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Secciones.Profiles
{
    public class SeccionProfile : Profile
    {
        public SeccionProfile()
        {
            CreateMap<Seccion, SeccionDto>();
            CreateMap<Seccion, SeccionSimpleDto>();
        }
    }
}
