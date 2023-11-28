using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Usuarios.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<Usuario, UsuarioSecurityDto>();
            CreateMap<Usuario, UsuarioSaveDto>().ReverseMap();
            CreateMap<Usuario, UsuarioFilterDto>().ReverseMap();

            CreateMap<PagedResult<Usuario>, PageResponse<UsuarioDto>>();
        }
    }
}
