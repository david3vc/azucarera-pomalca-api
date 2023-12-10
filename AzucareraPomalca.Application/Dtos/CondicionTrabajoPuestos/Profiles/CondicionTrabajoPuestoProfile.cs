using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.CondicionTrabajoPuestos.Profiles
{
    public class CondicionTrabajoPuestoProfile : Profile
    {
        public CondicionTrabajoPuestoProfile()
        {
            CreateMap<CondicionTrabajoPuesto, CondicionTrabajoPuestoDto>();
            CreateMap<CondicionTrabajoPuesto, CondicionTrabajoPuestoSaveDto>().ReverseMap();
        }
    }
}
