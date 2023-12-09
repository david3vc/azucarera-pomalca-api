using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Cursos.Profiles
{
    public class CursoProfile : Profile
    {
        public CursoProfile()
        {
            CreateMap<Curso, CursoDto>();
            CreateMap<Curso, CursoSaveDto>().ReverseMap();
            CreateMap<Curso, CursoFilterDto>().ReverseMap();

            CreateMap<PagedResult<Curso>, PageResponse<CursoDto>>();
        }
    }
}
