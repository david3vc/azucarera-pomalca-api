using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Responsabilidades.Profiles
{
    public class ResponsabilidadProfile : Profile
    {
        public ResponsabilidadProfile()
        {
            CreateMap<Responsabilidad, ResponsabilidadDto>();
            CreateMap<Responsabilidad, ResponsabilidadSimpleDto>();
        }
    }
}
