using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.CondicionEmpleados.Profiles
{
    public class CondicionEmpleadoProfile : Profile
    {
        public CondicionEmpleadoProfile()
        {
            CreateMap<CondicionEmpleado, CondicionEmpleadoDto>();
        }
    }
}
