using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.CapacitacionesEmpleados.Profiles
{
    public class CapacitacionEmpleadoProfile : Profile
    {
        public CapacitacionEmpleadoProfile()
        {
            CreateMap<CapacitacionEmpleado, CapacitacionEmpleadoDto>();
            CreateMap<CapacitacionEmpleado, CapacitacionEmpleadoSaveDto>().ReverseMap();
        }
    }
}
