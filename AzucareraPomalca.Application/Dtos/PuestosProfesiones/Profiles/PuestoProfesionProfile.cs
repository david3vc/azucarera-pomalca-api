using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.PuestosProfesiones.Profiles
{
    public class PuestoProfesionProfile : Profile
    {
        public PuestoProfesionProfile()
        {
            CreateMap<PuestoProfesion, PuestoProfesionDto>();
            CreateMap<PuestoProfesion, PuestoProfesionSaveDto>().ReverseMap();
        }
    }
}
