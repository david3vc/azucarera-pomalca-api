using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Capacitaciones.Profiles
{
    public class CapacitacionProfile : Profile
    {
        public CapacitacionProfile()
        {
            CreateMap<Capacitacion, CapacitacionDto>();
            CreateMap<Capacitacion, CapacitacionSaveDto>().ReverseMap();
            CreateMap<Capacitacion, CapacitacionFilterDto>().ReverseMap();

            CreateMap<PagedResult<Capacitacion>, PageResponse<CapacitacionDto>>();
        }
    }
}
