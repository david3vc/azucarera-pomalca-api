using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Puestos.Profiles
{
    public class PuestoProfile : Profile
    {
        public PuestoProfile()
        {
            CreateMap<Puesto, PuestoDto>();
            CreateMap<Puesto, PuestoSaveDto>().ReverseMap();
        }
    }
}
