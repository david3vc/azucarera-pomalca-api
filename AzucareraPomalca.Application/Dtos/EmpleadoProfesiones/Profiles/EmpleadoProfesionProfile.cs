using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.EmpleadoProfesiones.Profiles
{
    public class EmpleadoProfesionProfile : Profile
    {
        public EmpleadoProfesionProfile()
        {
            CreateMap<EmpleadoProfesion, EmpleadoProfesionDto>();
            CreateMap<EmpleadoProfesion, EmpleadoProfesionSaveDto>().ReverseMap();
        }
    }
}
