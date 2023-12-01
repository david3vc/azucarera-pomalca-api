using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.Misiones.Profiles
{
    public class MisionProfile : Profile
    {
        public MisionProfile()
        {
            CreateMap<Mision, MisionDto>();
            CreateMap<Mision, MisionSaveDto>().ReverseMap();
        }
    }
}
