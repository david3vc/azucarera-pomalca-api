using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Core.Paginations;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
