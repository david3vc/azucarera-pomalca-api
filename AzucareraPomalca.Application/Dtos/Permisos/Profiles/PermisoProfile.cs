using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Permisos.Profiles
{
    public class PermisoProfile : Profile
    {
        public PermisoProfile()
        {
            CreateMap<Permiso, PermisoDto>();
            CreateMap<Permiso, PermisoSaveDto>().ReverseMap();
        }
    }
}
