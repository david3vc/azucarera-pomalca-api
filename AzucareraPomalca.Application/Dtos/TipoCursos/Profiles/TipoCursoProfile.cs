using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.TipoCursos.Profiles
{
    public class TipoCursoProfile : Profile
    {
        public TipoCursoProfile()
        {
            CreateMap<TipoCurso, TipoCursoDto>();
            CreateMap<TipoCurso, TipoCursoSimpleDto>();
        }
    }
}
