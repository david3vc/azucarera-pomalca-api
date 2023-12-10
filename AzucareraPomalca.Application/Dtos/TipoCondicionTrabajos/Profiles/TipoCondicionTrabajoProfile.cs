using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.TipoCondicionTrabajos.Profiles
{
    public class TipoCondicionTrabajoProfile : Profile
    {
        public TipoCondicionTrabajoProfile()
        {
            CreateMap<TipoCondicionTrabajo, TipoCondicionTrabajoDto>();
        }
    }
}
