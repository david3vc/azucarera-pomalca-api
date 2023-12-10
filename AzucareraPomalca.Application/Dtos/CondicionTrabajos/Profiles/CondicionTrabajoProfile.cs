using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.CondicionTrabajos.Profiles
{
    public class CondicionTrabajoProfile : Profile
    {
        public CondicionTrabajoProfile()
        {
            CreateMap<CondicionTrabajo, CondicionTrabajoDto>();
        }
    }
}
