using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Empleados.Profiles
{
    public class EmpleadoSugeridoProfile : Profile
    {
        public EmpleadoSugeridoProfile()
        {
            CreateMap<EmpleadoSugerido, EmpleadoSugeridoDto>();
            CreateMap<EmpleadoSugerido, EmpleadoSugeridoFilterDto>().ReverseMap();
            CreateMap<PagedResult<EmpleadoSugerido>, PageResponse<EmpleadoSugeridoDto>>()
                .AfterMap<EmpleadoSugeridoProfileAction>();
        }
    }
}
