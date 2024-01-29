using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Secciones.Profiles
{
    public class SeccionProfile : Profile
    {
        public SeccionProfile()
        {
            CreateMap<Seccion, SeccionDto>();
            CreateMap<Seccion, SeccionSimpleDto>();
            CreateMap<Seccion, SeccionSaveDto>().ReverseMap();
            CreateMap<Seccion, SeccionFilterDto>().ReverseMap();
            CreateMap<PagedResult<Seccion>, PageResponse<SeccionDto>>();
        }
    }
}
