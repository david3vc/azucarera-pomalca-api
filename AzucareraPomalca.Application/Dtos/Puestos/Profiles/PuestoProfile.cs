using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Puestos.Profiles
{
    public class PuestoProfile : Profile
    {
        public PuestoProfile()
        {
            CreateMap<Puesto, PuestoDto>();
            //.AfterMap<PuestoProfileAction>();
            CreateMap<Puesto, PuestoSaveDto>().ReverseMap();
            CreateMap<PagedResult<Puesto>, PageResponse<PuestoDto>>();
        }
    }
}
