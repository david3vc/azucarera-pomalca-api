using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.ClaseOcupacionales.Profiles
{
    public class ClaseOcupacionalProfile : Profile
    {
        public ClaseOcupacionalProfile()
        {
            CreateMap<ClaseOcupacional, ClaseOcupacionalDto>();
            CreateMap<ClaseOcupacional, ClaseOcupacionalSimpleDto>();
        }
    }
}
