using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Roles.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<Rol, RolDto>();
            CreateMap<Rol, RolSaveDto>().ReverseMap();
            CreateMap<Rol, RolFilterDto>().ReverseMap();

            CreateMap<PagedResult<Rol>, PageResponse<RolDto>>();
        }
    }
}
