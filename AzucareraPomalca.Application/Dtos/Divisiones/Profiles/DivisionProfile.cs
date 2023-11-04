using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Divisiones.Profiles
{
    public class DivisionProfile : Profile
    {
        public DivisionProfile()
        {
            CreateMap<Division, DivisionDto>();
            CreateMap<Division, DivisionSimpleDto>();
        }
    }
}
