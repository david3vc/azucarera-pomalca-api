using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.PerfilCompetenciaEmpleados.Profiles
{
    public class PerfilCompetenciaEmpleadoProfile : Profile
    {
        public PerfilCompetenciaEmpleadoProfile()
        {
            CreateMap<PerfilCompetenciaEmpleado, PerfilCompetenciaEmpleadoDto>();
            CreateMap<PerfilCompetenciaEmpleado, PerfilCompetenciaEmpleadoSaveDto>().ReverseMap();
        }
    }
}
