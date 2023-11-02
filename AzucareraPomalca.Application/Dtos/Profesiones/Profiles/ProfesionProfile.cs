using AutoMapper;
using AzucareraPomalca.Core.Paginations;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Profesiones.Profiles
{
    public class ProfesionProfile : Profile
    {
        public ProfesionProfile()
        {
            CreateMap<Profesion, ProfesionDto>();
            CreateMap<Profesion, ProfesionSaveDto>().ReverseMap();
            CreateMap<Profesion, ProfesionFilterDto>().ReverseMap();

            CreateMap<ResponsePagination<Profesion>, ResponsePagination<ProfesionDto>>();
            CreateMap<RequestPagination<Profesion>, RequestPagination<ProfesionFilterDto>>().ReverseMap();
        }
    }
}
