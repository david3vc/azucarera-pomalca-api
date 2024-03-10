using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.TablaComunes.Profiles
{
    public class TablaComunProfile : Profile
    {
        public TablaComunProfile()
        {
            CreateMap<TablaComun, TablaComunDto>();
            CreateMap<TablaComun, TablaComunFilterDto>().ReverseMap();
        }
    }
}
