using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.EmpleadoCursos.Profiles
{
    public class EmpleadoCursoProfile : Profile
    {
        public EmpleadoCursoProfile()
        {
            CreateMap<EmpleadoCurso, EmpleadoCursoDto>();
            CreateMap<EmpleadoCurso, EmpleadoCursoSaveDto>().ReverseMap();
        }
    }
}
