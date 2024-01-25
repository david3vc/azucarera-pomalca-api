using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Divisiones.Profiles
{
    public class DivisionProfile : Profile
    {
        public DivisionProfile()
        {
            CreateMap<Division, DivisionDto>();
            CreateMap<Division, DivisionSimpleDto>();
            CreateMap<Division, DivisionSaveDto>().ReverseMap();
            CreateMap<Division, DivisionFilterDto>().ReverseMap();
            CreateMap<PagedResult<Division>, PageResponse<DivisionDto>>();
        }
    }
}
