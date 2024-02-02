using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Empleados.Profiles
{
    public class EmpleadoProfile : Profile
    {
        public EmpleadoProfile()
        {
            CreateMap<Empleado, EmpleadoDto>();
            CreateMap<Empleado, EmpleadoSaveDto>().ReverseMap();
            CreateMap<Empleado, EmpleadoFilterDto>().ReverseMap();
            CreateMap<PagedResult<Empleado>, PageResponse<EmpleadoDto>>();
        }
    }
}
