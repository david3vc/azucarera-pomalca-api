using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.PerfilCompetenciaEmpleados.Profiles
{
    public class PerfilCompetenciaEmpleadoProfile : Profile
    {
        public PerfilCompetenciaEmpleadoProfile()
        {
            CreateMap<PerfilCompetenciaEmpleado, PerfilCompetenciaEmpleadoDto>()
                .ForMember(d => d.Nivel, opt => opt.MapFrom(s => s.GradoDominio != null ? (int?)s.GradoDominio.Nivel : null))
                .ForMember(d => d.NombreCompetencia, opt => opt.MapFrom(s => s.Competencia != null ? s.Competencia.Nombre : null));
            CreateMap<PerfilCompetenciaEmpleado, PerfilCompetenciaEmpleadoSaveDto>().ReverseMap();
        }
    }
}
