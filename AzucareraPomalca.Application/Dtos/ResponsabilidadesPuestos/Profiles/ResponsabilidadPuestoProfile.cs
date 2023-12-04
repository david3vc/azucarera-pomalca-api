using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos.Profiles
{
    public class ResponsabilidadPuestoProfile : Profile
    {
        public ResponsabilidadPuestoProfile()
        {
            CreateMap<ResponsabilidadPuesto, ResponsabilidadPuestoDto>();
            CreateMap<ResponsabilidadPuesto, ResponsabilidadPuestoSaveDto>().ReverseMap();
        }
    }
}
