using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.PuestosCursos.Profiles
{
    public class PuestoCursoProfile : Profile
    {
        public PuestoCursoProfile()
        {
            CreateMap<PuestoCurso, PuestoCursoDto>();
            CreateMap<PuestoCurso, PuestoCursoSaveDto>().ReverseMap();
        }
    }
}
